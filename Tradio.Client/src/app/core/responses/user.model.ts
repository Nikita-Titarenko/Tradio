export interface UserModel {
  id: string;
  fullname: string;
  creditCount: number;
  cityName: string;
  countryName: string;
  email: string;
  emailConfirmed: boolean;
  isBanned: boolean;
  roleName: string;
}
