import { AuthGuard } from './_guards/auth.guard';
import { HasRoleDirective } from './_directives/has-role.directive';
import { appRoutes } from './routes';

import { MeetingResolver } from './_resolvers/meeting.resolver';
import { ReviewCycleResolver } from './_resolvers/review-cycle.resolver';
import { ContributionResolver } from './_resolvers/contribution.resolver';
import { ActionItemResolver } from './_resolvers/action-item.resolver';
import { ActionItemService } from './_services/action-item.service';
import { ActionListComponent } from './shared/action-list/action-list.component';
import { InitiativeListResolver } from './_resolvers/initiative-list.resolver';
import { InitiativeResolver } from './_resolvers/initiative.resolver';
import { YearListResolver } from './_resolvers/year-list.resolver';
import { MembersResolver } from './_resolvers/members.resolver';
import { ProfileResolver } from './_resolvers/profile.resolver';

import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ClickOutsideModule } from 'ng-click-outside';
import { FileUploadModule } from 'ng2-file-upload';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { DataTablesModule } from 'angular-datatables';
import { RatingModule } from 'ngx-bootstrap/rating';
import { CdkTableModule } from '@angular/cdk/table';
import { MatTableModule } from '@angular/material/table';
import { EditorModule } from './initiative-meeting/editor/editor.module';
import { ChartsModule } from 'ng2-charts';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { MembersComponent } from './members/members.component';
import { HomeComponent } from './home/home.component';
import { YearListComponent } from './year-list/year-list.component';
import { InitiativeListComponent } from './initiative-list/initiative-list.component';
import { InitiativeUpdateComponent } from './initiative-update/initiative-update.component';
import { InitiativeHomeComponent } from './initiative-home/initiative-home.component';
import { LeadComponent } from './lead/lead.component';
import { ReviewCycleComponent } from './review-cycle/review-cycle.component';
import { InitiativeMeetingComponent } from './initiative-meeting/initiative-meeting.component';
import { ActionDashboardComponent } from './action-dashboard/action-dashboard.component';
import { MeetingNotesComponent } from './initiative-meeting/meeting-notes/meeting-notes.component';

import { AlertifyService } from './_services/alertify.service';
import { AuthService } from './_services/auth.service';
import { JWTTokenService } from './_services/JWTToken.service';
import { InitiativeService } from './_services/initiative.service';
import { ReviewCycleService } from './_services/review-cycle.service';
import { MeetingService } from './_services/meeting.service';
import { EditProfileComponent } from './edit-profile/edit-profile.component';

export function tokenGetter(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    HasRoleDirective,
    ForbiddenComponent,
    MembersComponent,
    HomeComponent,
    YearListComponent,
    InitiativeListComponent,
    InitiativeUpdateComponent,
    InitiativeHomeComponent,
    ActionListComponent,
    LeadComponent,
    ReviewCycleComponent,
    InitiativeMeetingComponent,
    MeetingNotesComponent,
    ActionDashboardComponent,
    EditProfileComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    BrowserAnimationsModule,
    ClickOutsideModule,
    FileUploadModule,
    DataTablesModule,
    CdkTableModule,
    MatTableModule,
    EditorModule,
    ChartsModule,
    ProgressbarModule.forRoot(),
    RatingModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5001'],
        disallowedRoutes: ['localhost:5001/api/auth'],
      },
    }),
  ],
  providers: [
    AlertifyService,
    AuthService,
    JWTTokenService,
    InitiativeService,
    ActionItemService,
    ReviewCycleService,
    MeetingService,
    AuthGuard,
    ProfileResolver,
    MembersResolver,
    YearListResolver,
    InitiativeListResolver,
    InitiativeResolver,
    ActionItemResolver,
    ContributionResolver,
    ReviewCycleResolver,
    MeetingResolver,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
