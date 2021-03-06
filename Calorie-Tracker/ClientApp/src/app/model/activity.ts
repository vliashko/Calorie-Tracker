/**
 * Calorie Tracker API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * Contact: vladimir.lyashko02@gmail.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { ActivityExercise } from './activityExercise';
import { UserProfile } from './userProfile';

export interface Activity {
    id: string;
    name: string;
    moment: Date;
    totalCaloriesSpent: number;
    exercisesWithReps: Array<ActivityExercise>;
    userProfileId: string;
    userProfile?: UserProfile;
}
