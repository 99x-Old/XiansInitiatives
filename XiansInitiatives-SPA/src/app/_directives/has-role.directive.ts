import {
  Directive,
  Input,
  ViewContainerRef,
  TemplateRef,
  OnInit,
} from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JWTTokenService } from '../_services/JWTToken.service';

@Directive({
  selector: '[appHasRole]',
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible = false;
  jwtHelper = new JwtHelperService();

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templeteRef: TemplateRef<any>,
    public tokenService: JWTTokenService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {

    const userRole = this.tokenService.getUserRoles();

    if (!userRole) {
      this.viewContainerRef.clear();
    }

    if (this.authService.roleMatch(this.appHasRole)) {
      if (!this.isVisible) {
        this.isVisible = true;
        this.viewContainerRef.createEmbeddedView(this.templeteRef);
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();
      }
    }
  }
}
