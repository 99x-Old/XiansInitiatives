import { UserService } from 'src/app/_services/user.service';
import { User } from './../_models/User';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
})
export class MembersComponent implements OnInit {
  users: User[] = [];
  selectedUser: User;
  showModal: boolean;
  registerForm: FormGroup;
  submitted = false;
  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.users = data.user;
    });

    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      designation: ['', Validators.required],
      locked: ['false'],
      role: ['User'],
    });
  }
  updateUser(user): void {
    this.selectedUser = user;
    this.showModal = true;
  }

  deleteUser(id): void {
    this.userService.deleteUser(id).subscribe(
      (data: any) => {
        this.alertifyService.success('User deleted successfully!');
        this.hidePopup();
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }

  hidePopup(): void {
    this.showModal = false;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    this.userService.updateUser(this.selectedUser).subscribe(
      (data: any) => {
        this.alertifyService.success('User details updated!');
        this.hidePopup();
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }
}
