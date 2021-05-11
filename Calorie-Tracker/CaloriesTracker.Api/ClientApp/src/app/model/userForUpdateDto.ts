import { Gender } from './gender';
import { UserProfileForUpdateDto } from './userProfileForUpdateDto';

export interface UserForUpdateDto {
    userName: string;
    email: string;
    userProfile: UserProfileForUpdateDto;
}