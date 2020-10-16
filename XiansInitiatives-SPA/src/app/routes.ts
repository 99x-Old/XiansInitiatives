import { ActionDashboardComponent } from './action-dashboard/action-dashboard.component';
import { ReviewCycleComponent } from './review-cycle/review-cycle.component';
import { LeadComponent } from './lead/lead.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { ProfileResolver } from './_resolvers/profile.resolver';
import { MembersComponent } from './members/members.component';
import { MembersResolver } from './_resolvers/members.resolver';
import { YearListComponent } from './year-list/year-list.component';
import { YearListResolver } from './_resolvers/year-list.resolver';
import { InitiativeListComponent } from './initiative-list/initiative-list.component';
import { InitiativeListResolver } from './_resolvers/initiative-list.resolver';
import { InitiativeUpdateComponent } from './initiative-update/initiative-update.component';
import { InitiativeResolver } from './_resolvers/initiative.resolver';
import { InitiativeHomeComponent } from './initiative-home/initiative-home.component';
import { ActionItemResolver } from './_resolvers/action-item.resolver';
import { ContributionResolver } from './_resolvers/contribution.resolver';
import { ReviewCycleResolver } from './_resolvers/review-cycle.resolver';
import { InitiativeMeetingComponent } from './initiative-meeting/initiative-meeting.component';
import { MeetingResolver } from './_resolvers/meeting.resolver';
import { MeetingNotesComponent } from './initiative-meeting/meeting-notes/meeting-notes.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';

export const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'home/:id',
        component: HomeComponent,
        resolve: { user: ProfileResolver, actionItems: ContributionResolver },
        data: { roles: ['User', 'Admin', 'Evaluator', 'Lead'] },
      },
      {
        path: 'members',
        component: MembersComponent,
        resolve: { user: MembersResolver },
        data: { roles: ['Admin'] },
      },
      {
        path: 'manageinitives',
        component: YearListComponent,
        resolve: { initiativeYears: YearListResolver },
        data: { roles: ['Evaluator'] },
      },
      {
        path: 'initiativelist/:id',
        component: InitiativeListComponent,
        resolve: { initiatives: InitiativeListResolver },
        data: { roles: ['User', 'Admin', 'Evaluator', 'Lead'] },
      },
      {
        path: 'updateinitiative/:id',
        component: InitiativeUpdateComponent,
        resolve: { initiative: InitiativeResolver },
        data: { roles: ['Evaluator'] },
      },
      {
        path: 'initiativehome/:id',
        component: InitiativeHomeComponent,
        resolve: {
          initiative: InitiativeResolver,
          actionItems: ActionItemResolver,
          meetings: MeetingResolver,
        },
        data: { roles: ['User', 'Admin', 'Evaluator', 'Lead'] },
      },
      {
        path: 'reviewcycle/:id',
        component: ReviewCycleComponent,
        resolve: { reviewCycles: ReviewCycleResolver },
        data: { roles: ['Evaluator'] },
      },
      {
        path: 'leadinitiative',
        component: LeadComponent,
        data: { roles: ['Lead'] },
      },
      {
        path: 'meetings',
        component: InitiativeMeetingComponent,
        resolve: { user: ProfileResolver },
        data: { roles: ['User', 'Admin', 'Evaluator', 'Lead'] },
      },
      {
        path: 'meetings/:id',
        component: InitiativeMeetingComponent,
        resolve: { user: ProfileResolver },
        data: { roles: ['Lead'] },
      },
      {
        path: 'meetingnotes/:note',
        component: MeetingNotesComponent,
        data: { roles: ['User', 'Admin', 'Evaluator', 'Lead'] },
      },
      {
        path: 'dashboard',
        component: ActionDashboardComponent,
        data: { roles: ['Evaluator'] },
      },
      {
        path: 'editProfile/:id',
        component: EditProfileComponent,
        resolve: { user: ProfileResolver },
        data: { roles: ['User', 'Admin', 'Evaluator', 'Lead'] },
      },
    ],
  },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forbidden', component: ForbiddenComponent },

  { path: '**', redirectTo: 'login', pathMatch: 'full' },
];
