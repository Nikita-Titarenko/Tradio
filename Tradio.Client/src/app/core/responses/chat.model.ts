import { MessageModel } from './message.model';

export interface ChatModel {
  applicationUserServiceId: number;
  fullName: string;
  serviceName: string;
  messages: MessageModel[];
  applicationUserId: string;
  serviceId: number;
}
