export interface SignInResponseModel {
  userId: string;
  jwtToken: string;
  emailConfirmed: boolean;
}
