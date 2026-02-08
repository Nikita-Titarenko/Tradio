export interface ServiceModel {
  id: number;
  name: string;
  price: number;
  description: string;
  creationDateTime: Date;
  categoryId: number;
  applicationUserName: string;
  categoryName: string;
  isVisible: boolean;
}
