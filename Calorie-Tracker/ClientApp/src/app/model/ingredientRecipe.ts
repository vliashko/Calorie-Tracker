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
import { Ingredient } from './ingredient';
import { Recipe } from './recipe';

export interface IngredientRecipe { 
    id?: string;
    ingredientId?: string;
    ingredient?: Ingredient;
    recipeId?: string;
    recipe?: Recipe;
    grams?: number;
}