export interface User {
  id: string;
  username: string;
  firstName: string;
  lastName: string;
  fullName: string;
  designation: string;
  gender: string;
  email: string;
  profileImageUrl: Date;
  adress: string;
  role: string;
  locked: boolean;
}

export interface MeetingUser {
  id: string;
  name?: string;
  avatar?: string;
  role?: string;
}
