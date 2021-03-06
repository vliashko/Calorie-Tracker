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
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent } from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from '../encoder';

import { Observable } from 'rxjs';

import { EatingForCreateDto } from '../model/eatingForCreateDto';
import { EatingForUpdateDto } from '../model/eatingForUpdateDto';
import { EatingForUpdateDtoJsonPatchDocument } from '../model/eatingForUpdateDtoJsonPatchDocument';

import { BASE_PATH, COLLECTION_FORMATS } from '../variables';
import { Configuration } from '../configuration';


@Injectable()
export class EatingsService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        this.basePath = this.configuration.basePath;
        if (configuration) {
            this.configuration = configuration;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }

    /**
     *
     *
     * @param countDays
     * @param userId
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
     public apiEatingsCountDaysGet(countDays: number, userId?: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
     public apiEatingsCountDaysGet(countDays: number, userId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
     public apiEatingsCountDaysGet(countDays: number, userId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
     public apiEatingsCountDaysGet(countDays: number, userId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {
 
         if (countDays === null || countDays === undefined) {
             throw new Error('Required parameter countDays was null or undefined when calling apiUserprofilesCountDaysGet.');
         }
 
 
         let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
         if (userId !== undefined && userId !== null) {
             queryParameters = queryParameters.set('userId', userId as any);
         }
 
         let headers = this.defaultHeaders;
 
         // authentication (Bearer) required
         if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
             headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
         }
 
         // to determine the Accept header
         const httpHeaderAccepts: string[] = [
         ];
         const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
         if (httpHeaderAcceptSelected !== undefined) {
             headers = headers.set('Accept', httpHeaderAcceptSelected);
         }
 
         // to determine the Content-Type header
         const consumes: string[] = [
         ];
 
         return this.httpClient.request<any>('get', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings/days/${encodeURIComponent(String(countDays))}`,
             {
                 params: queryParameters,
                 withCredentials: this.configuration.withCredentials,
                 headers,
                 observe,
                 reportProgress
             }
         );
     }

    /**
     *
     *
     * @param eatingId
     * @param userId
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdEatingsEatingIdDelete(eatingId: string, userId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdEatingsEatingIdDelete(eatingId: string, userId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdEatingsEatingIdDelete(eatingId: string, userId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdEatingsEatingIdDelete(eatingId: string, userId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (eatingId === null || eatingId === undefined) {
            throw new Error('Required parameter eatingId was null or undefined when calling apiUsersUserIdEatingsEatingIdDelete.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdEatingsEatingIdDelete.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
            headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
        }

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('delete', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings/${encodeURIComponent(String(eatingId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

    /**
     *
     *
     * @param eatingId
     * @param userId
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdEatingsEatingIdPatch(eatingId: string, userId: string, body?: EatingForUpdateDtoJsonPatchDocument, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdEatingsEatingIdPatch(eatingId: string, userId: string, body?: EatingForUpdateDtoJsonPatchDocument, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdEatingsEatingIdPatch(eatingId: string, userId: string, body?: EatingForUpdateDtoJsonPatchDocument, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdEatingsEatingIdPatch(eatingId: string, userId: string, body?: EatingForUpdateDtoJsonPatchDocument, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (eatingId === null || eatingId === undefined) {
            throw new Error('Required parameter eatingId was null or undefined when calling apiUsersUserIdEatingsEatingIdPatch.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdEatingsEatingIdPatch.');
        }


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
            headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
        }

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('patch', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings/${encodeURIComponent(String(eatingId))}`,
            {
                body,
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

    /**
     *
     *
     * @param eatingId
     * @param userId
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdEatingsEatingIdPut(eatingId: string, userId: string, body?: EatingForUpdateDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdEatingsEatingIdPut(eatingId: string, userId: string, body?: EatingForUpdateDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdEatingsEatingIdPut(eatingId: string, userId: string, body?: EatingForUpdateDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdEatingsEatingIdPut(eatingId: string, userId: string, body?: EatingForUpdateDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (eatingId === null || eatingId === undefined) {
            throw new Error('Required parameter eatingId was null or undefined when calling apiUsersUserIdEatingsEatingIdPut.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdEatingsEatingIdPut.');
        }


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
            headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
        }

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('put', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings/${encodeURIComponent(String(eatingId))}`,
            {
                body,
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

    /**
     *
     *
     * @param userId
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdEatingsGet(userId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdEatingsGet(userId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdEatingsGet(userId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdEatingsGet(userId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdEatingsGet.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
            headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
        }

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('get', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings`,
            {
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

    /**
     *
     *
     * @param userId
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdEatingsPost(userId: string, body?: EatingForCreateDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdEatingsPost(userId: string, body?: EatingForCreateDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdEatingsPost(userId: string, body?: EatingForCreateDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdEatingsPost(userId: string, body?: EatingForCreateDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdEatingsPost.');
        }


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
            headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
        }

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('post', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings`,
            {
                body,
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

    /**
     *
     *
     * @param eatingId
     * @param userId
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getEating(eatingId: string, userId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public getEating(eatingId: string, userId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public getEating(eatingId: string, userId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public getEating(eatingId: string, userId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (eatingId === null || eatingId === undefined) {
            throw new Error('Required parameter eatingId was null or undefined when calling getEating.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling getEating.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys.Authorization) {
            headers = headers.set('Authorization', this.configuration.apiKeys.Authorization);
        }

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('get', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/eatings/${encodeURIComponent(String(eatingId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

}
