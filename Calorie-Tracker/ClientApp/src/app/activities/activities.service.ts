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

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { ActivityForCreateDto } from '../model/activityForCreateDto';
import { ActivityForUpdateDto } from '../model/activityForUpdateDto';
import { ActivityForUpdateDtoJsonPatchDocument } from '../model/activityForUpdateDtoJsonPatchDocument';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class ActivitiesService {

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
         public apiActivitiesCountDaysGet(countDays: number, userId?: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
         public apiActivitiesCountDaysGet(countDays: number, userId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
         public apiActivitiesCountDaysGet(countDays: number, userId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
         public apiActivitiesCountDaysGet(countDays: number, userId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {
     
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
     
             return this.httpClient.request<any>('get', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities/days/${encodeURIComponent(String(countDays))}`,
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
     * @param activityId
     * @param userId
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdActivitiesActivityIdDelete(activityId: string, userId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdActivitiesActivityIdDelete(activityId: string, userId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdActivitiesActivityIdDelete(activityId: string, userId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdActivitiesActivityIdDelete(activityId: string, userId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (activityId === null || activityId === undefined) {
            throw new Error('Required parameter activityId was null or undefined when calling apiUsersUserIdActivitiesActivityIdDelete.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdActivitiesActivityIdDelete.');
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

        return this.httpClient.request<any>('delete', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities/${encodeURIComponent(String(activityId))}`,
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
     * @param activityId
     * @param userId
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdActivitiesActivityIdPatch(activityId: string, userId: string, body?: ActivityForUpdateDtoJsonPatchDocument, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdActivitiesActivityIdPatch(activityId: string, userId: string, body?: ActivityForUpdateDtoJsonPatchDocument, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdActivitiesActivityIdPatch(activityId: string, userId: string, body?: ActivityForUpdateDtoJsonPatchDocument, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdActivitiesActivityIdPatch(activityId: string, userId: string, body?: ActivityForUpdateDtoJsonPatchDocument, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (activityId === null || activityId === undefined) {
            throw new Error('Required parameter activityId was null or undefined when calling apiUsersUserIdActivitiesActivityIdPatch.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdActivitiesActivityIdPatch.');
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

        return this.httpClient.request<any>('patch', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities/${encodeURIComponent(String(activityId))}`,
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
     * @param activityId
     * @param userId
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUsersUserIdActivitiesActivityIdPut(activityId: string, userId: string, body?: ActivityForUpdateDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdActivitiesActivityIdPut(activityId: string, userId: string, body?: ActivityForUpdateDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdActivitiesActivityIdPut(activityId: string, userId: string, body?: ActivityForUpdateDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdActivitiesActivityIdPut(activityId: string, userId: string, body?: ActivityForUpdateDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (activityId === null || activityId === undefined) {
            throw new Error('Required parameter activityId was null or undefined when calling apiUsersUserIdActivitiesActivityIdPut.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdActivitiesActivityIdPut.');
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

        return this.httpClient.request<any>('put', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities/${encodeURIComponent(String(activityId))}`,
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
    public apiUsersUserIdActivitiesGet(userId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdActivitiesGet(userId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdActivitiesGet(userId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdActivitiesGet(userId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdActivitiesGet.');
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

        return this.httpClient.request<any>('get', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities`,
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
    public apiUsersUserIdActivitiesPost(userId: string, body?: ActivityForCreateDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiUsersUserIdActivitiesPost(userId: string, body?: ActivityForCreateDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiUsersUserIdActivitiesPost(userId: string, body?: ActivityForCreateDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiUsersUserIdActivitiesPost(userId: string, body?: ActivityForCreateDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling apiUsersUserIdActivitiesPost.');
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

        return this.httpClient.request<any>('post', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities`,
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
     * @param activityId
     * @param userId
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getActivity(activityId: string, userId: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public getActivity(activityId: string, userId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public getActivity(activityId: string, userId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public getActivity(activityId: string, userId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (activityId === null || activityId === undefined) {
            throw new Error('Required parameter activityId was null or undefined when calling getActivity.');
        }

        if (userId === null || userId === undefined) {
            throw new Error('Required parameter userId was null or undefined when calling getActivity.');
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

        return this.httpClient.request<any>('get', `${this.basePath}/api/users/${encodeURIComponent(String(userId))}/activities/${encodeURIComponent(String(activityId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers,
                observe,
                reportProgress
            }
        );
    }

}
