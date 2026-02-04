import { MessageModel } from './message.model';

export interface ChatModel {
  fullName: string;
  serviceName: string;
  messages: MessageModel[];
}
