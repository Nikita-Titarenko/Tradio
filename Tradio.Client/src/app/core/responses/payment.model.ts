export interface PaymentModel {
  id: number;
  applicationUserServiceId: number;
  price: number;
  creationDateTime: string | Date;
  serviceName: string;
  areYouProvider: boolean;
}
