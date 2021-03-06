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
import { Activity } from './activity';
import { Eating } from './eating';
import { Gender } from './gender';
import { Recipe } from './recipe';
import { User } from './user';

export interface UserProfile { 
    id?: string;
    weight?: number;
    height?: number;
    gender?: Gender;
    dateOfBirth?: Date;
    calories?: number;
    currentCalories?: number;
    userId?: string;
    user?: User;
    eatings?: Array<Eating>;
    activities?: Array<Activity>;
    recipes?: Array<Recipe>;
}