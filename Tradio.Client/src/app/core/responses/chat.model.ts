import { MessageModel } from './message.model';

export interface ChatModel {
  applicationUserServiceId?: number;
  isRecipient: boolean;
  fullName: string;
  serviceName: string;
  messages: MessageModel[];
  applicationUserId: string;
  price: number;
  serviceId: number;
}
