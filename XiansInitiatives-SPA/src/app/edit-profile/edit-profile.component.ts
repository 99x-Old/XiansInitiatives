import { User } from './../_models/User';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
})
export class EditProfileComponent implements OnInit {
  user: User;
  bsConfig: Partial<BsDatepickerConfig>;
  editProfileForm: FormGroup;
  previousDate: Date;
  public progress: number;
  public message: string;

  @Output() public onUploadFinished = new EventEmitter<any>();

  constructor(
    private alertifyService: AlertifyService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.user = data.user;
    });
    this.createeditProfileForm();
  }

  createeditProfileForm(): void {
    this.editProfileForm = this.formBuilder.group({
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      designation: ['', Validators.required],
    });
  }

  passwordMatchValidator(g: FormGroup): any {
    return g.get('password').value === g.get('confirmPassword').value
      ? null
      : { mismatch: true };
  }

  updateProfile(): void {
    if (this.editProfileForm.invalid) {
      return;
    }

    this.userService.updateUser(this.user).subscribe(
      (data: any) => {
        this.alertifyService.success('User details updated!');
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    const fileToUpload = files[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.userService.updateProfilePicture(this.user.id, formData).subscribe(
      (event) => {
        this.alertifyService.success('Image uploaded successful');
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round((100 * event.loaded) / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      },
      (error) => {
        this.alertifyService.error('Failed to save the image');
      }
    );
  }
}
