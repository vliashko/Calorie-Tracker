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
import { Exercise } from './exercise';

export interface ActivityExerciseForCreateDto { 
    name: string;
    description: string;
    caloriesSpent: number;
    numberOfRepetitions?: number;
    numberOfSets?: number;
}