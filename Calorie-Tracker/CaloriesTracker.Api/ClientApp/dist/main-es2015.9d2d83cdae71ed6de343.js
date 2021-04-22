(window.webpackJsonp=window.webpackJsonp||[]).push([[1],{0:function(t,e,n){t.exports=n("zUnb")},zUnb:function(t,e,n){"use strict";function r(t){return"function"==typeof t}n.r(e);let s=!1;const o={Promise:void 0,set useDeprecatedSynchronousErrorHandling(t){if(t){const t=new Error;console.warn("DEPRECATED! RxJS was set to use deprecated synchronous error handling behavior by code at: \n"+t.stack)}else s&&console.log("RxJS: Back to a better error behavior. Thank you. <3");s=t},get useDeprecatedSynchronousErrorHandling(){return s}};function i(t){setTimeout(()=>{throw t},0)}const l={closed:!0,next(t){},error(t){if(o.useDeprecatedSynchronousErrorHandling)throw t;i(t)},complete(){}},u=(()=>Array.isArray||(t=>t&&"number"==typeof t.length))();function a(t){return null!==t&&"object"==typeof t}const c=(()=>{function t(t){return Error.call(this),this.message=t?`${t.length} errors occurred during unsubscription:\n${t.map((t,e)=>`${e+1}) ${t.toString()}`).join("\n  ")}`:"",this.name="UnsubscriptionError",this.errors=t,this}return t.prototype=Object.create(Error.prototype),t})();let h=(()=>{class t{constructor(t){this.closed=!1,this._parentOrParents=null,this._subscriptions=null,t&&(this._ctorUnsubscribe=!0,this._unsubscribe=t)}unsubscribe(){let e;if(this.closed)return;let{_parentOrParents:n,_ctorUnsubscribe:s,_unsubscribe:o,_subscriptions:i}=this;if(this.closed=!0,this._parentOrParents=null,this._subscriptions=null,n instanceof t)n.remove(this);else if(null!==n)for(let t=0;t<n.length;++t)n[t].remove(this);if(r(o)){s&&(this._unsubscribe=void 0);try{o.call(this)}catch(l){e=l instanceof c?d(l.errors):[l]}}if(u(i)){let t=-1,n=i.length;for(;++t<n;){const n=i[t];if(a(n))try{n.unsubscribe()}catch(l){e=e||[],l instanceof c?e=e.concat(d(l.errors)):e.push(l)}}}if(e)throw new c(e)}add(e){let n=e;if(!e)return t.EMPTY;switch(typeof e){case"function":n=new t(e);case"object":if(n===this||n.closed||"function"!=typeof n.unsubscribe)return n;if(this.closed)return n.unsubscribe(),n;if(!(n instanceof t)){const e=n;n=new t,n._subscriptions=[e]}break;default:throw new Error("unrecognized teardown "+e+" added to Subscription.")}let{_parentOrParents:r}=n;if(null===r)n._parentOrParents=this;else if(r instanceof t){if(r===this)return n;n._parentOrParents=[r,this]}else{if(-1!==r.indexOf(this))return n;r.push(this)}const s=this._subscriptions;return null===s?this._subscriptions=[n]:s.push(n),n}remove(t){const e=this._subscriptions;if(e){const n=e.indexOf(t);-1!==n&&e.splice(n,1)}}}return t.EMPTY=function(t){return t.closed=!0,t}(new t),t})();function d(t){return t.reduce((t,e)=>t.concat(e instanceof c?e.errors:e),[])}const f=(()=>"function"==typeof Symbol?Symbol("rxSubscriber"):"@@rxSubscriber_"+Math.random())();class p extends h{constructor(t,e,n){switch(super(),this.syncErrorValue=null,this.syncErrorThrown=!1,this.syncErrorThrowable=!1,this.isStopped=!1,arguments.length){case 0:this.destination=l;break;case 1:if(!t){this.destination=l;break}if("object"==typeof t){t instanceof p?(this.syncErrorThrowable=t.syncErrorThrowable,this.destination=t,t.add(this)):(this.syncErrorThrowable=!0,this.destination=new _(this,t));break}default:this.syncErrorThrowable=!0,this.destination=new _(this,t,e,n)}}[f](){return this}static create(t,e,n){const r=new p(t,e,n);return r.syncErrorThrowable=!1,r}next(t){this.isStopped||this._next(t)}error(t){this.isStopped||(this.isStopped=!0,this._error(t))}complete(){this.isStopped||(this.isStopped=!0,this._complete())}unsubscribe(){this.closed||(this.isStopped=!0,super.unsubscribe())}_next(t){this.destination.next(t)}_error(t){this.destination.error(t),this.unsubscribe()}_complete(){this.destination.complete(),this.unsubscribe()}_unsubscribeAndRecycle(){const{_parentOrParents:t}=this;return this._parentOrParents=null,this.unsubscribe(),this.closed=!1,this.isStopped=!1,this._parentOrParents=t,this}}class _ extends p{constructor(t,e,n,s){let o;super(),this._parentSubscriber=t;let i=this;r(e)?o=e:e&&(o=e.next,n=e.error,s=e.complete,e!==l&&(i=Object.create(e),r(i.unsubscribe)&&this.add(i.unsubscribe.bind(i)),i.unsubscribe=this.unsubscribe.bind(this))),this._context=i,this._next=o,this._error=n,this._complete=s}next(t){if(!this.isStopped&&this._next){const{_parentSubscriber:e}=this;o.useDeprecatedSynchronousErrorHandling&&e.syncErrorThrowable?this.__tryOrSetError(e,this._next,t)&&this.unsubscribe():this.__tryOrUnsub(this._next,t)}}error(t){if(!this.isStopped){const{_parentSubscriber:e}=this,{useDeprecatedSynchronousErrorHandling:n}=o;if(this._error)n&&e.syncErrorThrowable?(this.__tryOrSetError(e,this._error,t),this.unsubscribe()):(this.__tryOrUnsub(this._error,t),this.unsubscribe());else if(e.syncErrorThrowable)n?(e.syncErrorValue=t,e.syncErrorThrown=!0):i(t),this.unsubscribe();else{if(this.unsubscribe(),n)throw t;i(t)}}}complete(){if(!this.isStopped){const{_parentSubscriber:t}=this;if(this._complete){const e=()=>this._complete.call(this._context);o.useDeprecatedSynchronousErrorHandling&&t.syncErrorThrowable?(this.__tryOrSetError(t,e),this.unsubscribe()):(this.__tryOrUnsub(e),this.unsubscribe())}else this.unsubscribe()}}__tryOrUnsub(t,e){try{t.call(this._context,e)}catch(n){if(this.unsubscribe(),o.useDeprecatedSynchronousErrorHandling)throw n;i(n)}}__tryOrSetError(t,e,n){if(!o.useDeprecatedSynchronousErrorHandling)throw new Error("bad call");try{e.call(this._context,n)}catch(r){return o.useDeprecatedSynchronousErrorHandling?(t.syncErrorValue=r,t.syncErrorThrown=!0,!0):(i(r),!0)}return!1}_unsubscribe(){const{_parentSubscriber:t}=this;this._context=null,this._parentSubscriber=null,t.unsubscribe()}}const g=(()=>"function"==typeof Symbol&&Symbol.observable||"@@observable")();function y(t){return t}let m=(()=>{class t{constructor(t){this._isScalar=!1,t&&(this._subscribe=t)}lift(e){const n=new t;return n.source=this,n.operator=e,n}subscribe(t,e,n){const{operator:r}=this,s=function(t,e,n){if(t){if(t instanceof p)return t;if(t[f])return t[f]()}return t||e||n?new p(t,e,n):new p(l)}(t,e,n);if(s.add(r?r.call(s,this.source):this.source||o.useDeprecatedSynchronousErrorHandling&&!s.syncErrorThrowable?this._subscribe(s):this._trySubscribe(s)),o.useDeprecatedSynchronousErrorHandling&&s.syncErrorThrowable&&(s.syncErrorThrowable=!1,s.syncErrorThrown))throw s.syncErrorValue;return s}_trySubscribe(t){try{return this._subscribe(t)}catch(e){o.useDeprecatedSynchronousErrorHandling&&(t.syncErrorThrown=!0,t.syncErrorValue=e),function(t){for(;t;){const{closed:e,destination:n,isStopped:r}=t;if(e||r)return!1;t=n&&n instanceof p?n:null}return!0}(t)?t.error(e):console.warn(e)}}forEach(t,e){return new(e=v(e))((e,n)=>{let r;r=this.subscribe(e=>{try{t(e)}catch(s){n(s),r&&r.unsubscribe()}},n,e)})}_subscribe(t){const{source:e}=this;return e&&e.subscribe(t)}[g](){return this}pipe(...t){return 0===t.length?this:(0===(e=t).length?y:1===e.length?e[0]:function(t){return e.reduce((t,e)=>e(t),t)})(this);var e}toPromise(t){return new(t=v(t))((t,e)=>{let n;this.subscribe(t=>n=t,t=>e(t),()=>t(n))})}}return t.create=e=>new t(e),t})();function v(t){if(t||(t=o.Promise||Promise),!t)throw new Error("no Promise impl found");return t}const b=(()=>{function t(){return Error.call(this),this.message="object unsubscribed",this.name="ObjectUnsubscribedError",this}return t.prototype=Object.create(Error.prototype),t})();class w extends h{constructor(t,e){super(),this.subject=t,this.subscriber=e,this.closed=!1}unsubscribe(){if(this.closed)return;this.closed=!0;const t=this.subject,e=t.observers;if(this.subject=null,!e||0===e.length||t.isStopped||t.closed)return;const n=e.indexOf(this.subscriber);-1!==n&&e.splice(n,1)}}class C extends p{constructor(t){super(t),this.destination=t}}let E=(()=>{class t extends m{constructor(){super(),this.observers=[],this.closed=!1,this.isStopped=!1,this.hasError=!1,this.thrownError=null}[f](){return new C(this)}lift(t){const e=new A(this,this);return e.operator=t,e}next(t){if(this.closed)throw new b;if(!this.isStopped){const{observers:e}=this,n=e.length,r=e.slice();for(let s=0;s<n;s++)r[s].next(t)}}error(t){if(this.closed)throw new b;this.hasError=!0,this.thrownError=t,this.isStopped=!0;const{observers:e}=this,n=e.length,r=e.slice();for(let s=0;s<n;s++)r[s].error(t);this.observers.length=0}complete(){if(this.closed)throw new b;this.isStopped=!0;const{observers:t}=this,e=t.length,n=t.slice();for(let r=0;r<e;r++)n[r].complete();this.observers.length=0}unsubscribe(){this.isStopped=!0,this.closed=!0,this.observers=null}_trySubscribe(t){if(this.closed)throw new b;return super._trySubscribe(t)}_subscribe(t){if(this.closed)throw new b;return this.hasError?(t.error(this.thrownError),h.EMPTY):this.isStopped?(t.complete(),h.EMPTY):(this.observers.push(t),new w(this,t))}asObservable(){const t=new m;return t.source=this,t}}return t.create=(t,e)=>new A(t,e),t})();class A extends E{constructor(t,e){super(),this.destination=t,this.source=e}next(t){const{destination:e}=this;e&&e.next&&e.next(t)}error(t){const{destination:e}=this;e&&e.error&&this.destination.error(t)}complete(){const{destination:t}=this;t&&t.complete&&this.destination.complete()}_subscribe(t){const{source:e}=this;return e?this.source.subscribe(t):h.EMPTY}}function k(t,e){return function(n){if("function"!=typeof t)throw new TypeError("argument is not a function. Are you looking for `mapTo()`?");return n.lift(new x(t,e))}}class x{constructor(t,e){this.project=t,this.thisArg=e}call(t,e){return e.subscribe(new S(t,this.project,this.thisArg))}}class S extends p{constructor(t,e,n){super(t),this.project=e,this.count=0,this.thisArg=n||this}_next(t){let e;try{e=this.project.call(this.thisArg,t,this.count++)}catch(n){return void this.destination.error(n)}this.destination.next(e)}}const T=t=>e=>{for(let n=0,r=t.length;n<r&&!e.closed;n++)e.next(t[n]);e.complete()};function I(){return"function"==typeof Symbol&&Symbol.iterator?Symbol.iterator:"@@iterator"}const V=I(),O=t=>t&&"number"==typeof t.length&&"function"!=typeof t;function D(t){return!!t&&"function"!=typeof t.subscribe&&"function"==typeof t.then}const P=t=>{if(t&&"function"==typeof t[g])return r=t,t=>{const e=r[g]();if("function"!=typeof e.subscribe)throw new TypeError("Provided object does not correctly implement Symbol.observable");return e.subscribe(t)};if(O(t))return T(t);if(D(t))return n=t,t=>(n.then(e=>{t.closed||(t.next(e),t.complete())},e=>t.error(e)).then(null,i),t);if(t&&"function"==typeof t[V])return e=t,t=>{const n=e[V]();for(;;){let e;try{e=n.next()}catch(r){return t.error(r),t}if(e.done){t.complete();break}if(t.next(e.value),t.closed)break}return"function"==typeof n.return&&t.add(()=>{n.return&&n.return()}),t};{const e=a(t)?"an invalid object":`'${t}'`;throw new TypeError(`You provided ${e} where a stream was expected. You can provide an Observable, Promise, Array, or Iterable.`)}var e,n,r};function j(t,e){return new m(n=>{const r=new h;let s=0;return r.add(e.schedule(function(){s!==t.length?(n.next(t[s++]),n.closed||r.add(this.schedule())):n.complete()})),r})}function M(t,e){return e?function(t,e){if(null!=t){if(function(t){return t&&"function"==typeof t[g]}(t))return function(t,e){return new m(n=>{const r=new h;return r.add(e.schedule(()=>{const s=t[g]();r.add(s.subscribe({next(t){r.add(e.schedule(()=>n.next(t)))},error(t){r.add(e.schedule(()=>n.error(t)))},complete(){r.add(e.schedule(()=>n.complete()))}}))})),r})}(t,e);if(D(t))return function(t,e){return new m(n=>{const r=new h;return r.add(e.schedule(()=>t.then(t=>{r.add(e.schedule(()=>{n.next(t),r.add(e.schedule(()=>n.complete()))}))},t=>{r.add(e.schedule(()=>n.error(t)))}))),r})}(t,e);if(O(t))return j(t,e);if(function(t){return t&&"function"==typeof t[V]}(t)||"string"==typeof t)return function(t,e){if(!t)throw new Error("Iterable cannot be null");return new m(n=>{const r=new h;let s;return r.add(()=>{s&&"function"==typeof s.return&&s.return()}),r.add(e.schedule(()=>{s=t[V](),r.add(e.schedule(function(){if(n.closed)return;let t,e;try{const n=s.next();t=n.value,e=n.done}catch(r){return void n.error(r)}e?n.complete():(n.next(t),this.schedule())}))})),r})}(t,e)}throw new TypeError((null!==t&&typeof t||t)+" is not observable")}(t,e):t instanceof m?t:new m(P(t))}class N extends p{constructor(t){super(),this.parent=t}_next(t){this.parent.notifyNext(t)}_error(t){this.parent.notifyError(t),this.unsubscribe()}_complete(){this.parent.notifyComplete(),this.unsubscribe()}}class H extends p{notifyNext(t){this.destination.next(t)}notifyError(t){this.destination.error(t)}notifyComplete(){this.destination.complete()}}function R(t,e,n=Number.POSITIVE_INFINITY){return"function"==typeof e?r=>r.pipe(R((n,r)=>M(t(n,r)).pipe(k((t,s)=>e(n,t,r,s))),n)):("number"==typeof e&&(n=e),e=>e.lift(new F(t,n)))}class F{constructor(t,e=Number.POSITIVE_INFINITY){this.project=t,this.concurrent=e}call(t,e){return e.subscribe(new L(t,this.project,this.concurrent))}}class L extends H{constructor(t,e,n=Number.POSITIVE_INFINITY){super(t),this.project=e,this.concurrent=n,this.hasCompleted=!1,this.buffer=[],this.active=0,this.index=0}_next(t){this.active<this.concurrent?this._tryNext(t):this.buffer.push(t)}_tryNext(t){let e;const n=this.index++;try{e=this.project(t,n)}catch(r){return void this.destination.error(r)}this.active++,this._innerSub(e)}_innerSub(t){const e=new N(this),n=this.destination;n.add(e);const r=function(t,e){if(e.closed)return;if(t instanceof m)return t.subscribe(e);let n;try{n=P(t)(e)}catch(r){e.error(r)}return n}(t,e);r!==e&&n.add(r)}_complete(){this.hasCompleted=!0,0===this.active&&0===this.buffer.length&&this.destination.complete(),this.unsubscribe()}notifyNext(t){this.destination.next(t)}notifyComplete(){const t=this.buffer;this.active--,t.length>0?this._next(t.shift()):0===this.active&&this.hasCompleted&&this.destination.complete()}}function B(...t){let e=Number.POSITIVE_INFINITY,n=null,r=t[t.length-1];var s;return(s=r)&&"function"==typeof s.schedule?(n=t.pop(),t.length>1&&"number"==typeof t[t.length-1]&&(e=t.pop())):"number"==typeof r&&(e=t.pop()),null===n&&1===t.length&&t[0]instanceof m?t[0]:function(t=Number.POSITIVE_INFINITY){return R(y,t)}(e)(function(t,e){return e?j(t,e):new m(T(t))}(t,n))}function U(){return function(t){return t.lift(new Z(t))}}class Z{constructor(t){this.connectable=t}call(t,e){const{connectable:n}=this;n._refCount++;const r=new $(t,n),s=e.subscribe(r);return r.closed||(r.connection=n.connect()),s}}class $ extends p{constructor(t,e){super(t),this.connectable=e}_unsubscribe(){const{connectable:t}=this;if(!t)return void(this.connection=null);this.connectable=null;const e=t._refCount;if(e<=0)return void(this.connection=null);if(t._refCount=e-1,e>1)return void(this.connection=null);const{connection:n}=this,r=t._connection;this.connection=null,!r||n&&r!==n||r.unsubscribe()}}class z extends m{constructor(t,e){super(),this.source=t,this.subjectFactory=e,this._refCount=0,this._isComplete=!1}_subscribe(t){return this.getSubject().subscribe(t)}getSubject(){const t=this._subject;return t&&!t.isStopped||(this._subject=this.subjectFactory()),this._subject}connect(){let t=this._connection;return t||(this._isComplete=!1,t=this._connection=new h,t.add(this.source.subscribe(new W(this.getSubject(),this))),t.closed&&(this._connection=null,t=h.EMPTY)),t}refCount(){return U()(this)}}const G=(()=>{const t=z.prototype;return{operator:{value:null},_refCount:{value:0,writable:!0},_subject:{value:null,writable:!0},_connection:{value:null,writable:!0},_subscribe:{value:t._subscribe},_isComplete:{value:t._isComplete,writable:!0},getSubject:{value:t.getSubject},connect:{value:t.connect},refCount:{value:t.refCount}}})();class W extends C{constructor(t,e){super(t),this.connectable=e}_error(t){this._unsubscribe(),super._error(t)}_complete(){this.connectable._isComplete=!0,this._unsubscribe(),super._complete()}_unsubscribe(){const t=this.connectable;if(t){this.connectable=null;const e=t._connection;t._refCount=0,t._subject=null,t._connection=null,e&&e.unsubscribe()}}}function Q(){return new E}
/**
 * @license Angular v11.2.11
 * (c) 2010-2021 Google LLC. https://angular.io/
 * License: MIT
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function q(t){for(let e in t)if(t[e]===q)return e;throw Error("Could not find renamed property on target object.")}function J(t,e){for(const n in e)e.hasOwnProperty(n)&&!t.hasOwnProperty(n)&&(t[n]=e[n])}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function K(t){if("string"==typeof t)return t;if(Array.isArray(t))return"["+t.map(K).join(", ")+"]";if(null==t)return""+t;if(t.overriddenName)return`${t.overriddenName}`;if(t.name)return`${t.name}`;const e=t.toString();if(null==e)return""+e;const n=e.indexOf("\n");return-1===n?e:e.substring(0,n)}function Y(t,e){return null==t||""===t?null===e?"":e:null==e||""===e?t:t+" "+e}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const X=q({__forward_ref__:q});function tt(t){return t.__forward_ref__=tt,t.toString=function(){return K(this())},t}function et(t){return nt(t)?t():t}function nt(t){return"function"==typeof t&&t.hasOwnProperty(X)&&t.__forward_ref__===tt}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class rt extends Error{constructor(t,e){super(function(t,e){return`${t?`NG0${t}: `:""}${e}`
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}(t,e)),this.code=t}}function st(t){return"string"==typeof t?t:null==t?"":String(t)}function ot(t){return"function"==typeof t?t.name||t.toString():"object"==typeof t&&null!=t&&"function"==typeof t.type?t.type.name||t.type.toString():st(t)}function it(t,e){const n=e?` in ${e}`:"";throw new rt("201",`No provider for ${ot(t)} found${n}`)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function lt(t){return{token:t.token,providedIn:t.providedIn||null,factory:t.factory,value:void 0}}function ut(t){return{providers:t.providers||[],imports:t.imports||[]}}function at(t){return ct(t,dt)||ct(t,pt)}function ct(t,e){return t.hasOwnProperty(e)?t[e]:null}function ht(t){return t&&(t.hasOwnProperty(ft)||t.hasOwnProperty(_t))?t[ft]:null}const dt=q({"\u0275prov":q}),ft=q({"\u0275inj":q}),pt=q({ngInjectableDef:q}),_t=q({ngInjectorDef:q});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */var gt=function(t){return t[t.Default=0]="Default",t[t.Host=1]="Host",t[t.Self=2]="Self",t[t.SkipSelf=4]="SkipSelf",t[t.Optional=8]="Optional",t}({});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let yt;function mt(t){const e=yt;return yt=t,e}function vt(t,e,n){const r=at(t);return r&&"root"==r.providedIn?void 0===r.value?r.value=r.factory():r.value:n&gt.Optional?null:void 0!==e?e:void it(K(t),"Injector")}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function bt(t){return{toString:t}.toString()}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */var wt=function(t){return t[t.OnPush=0]="OnPush",t[t.Default=1]="Default",t}({}),Ct=function(t){return t[t.Emulated=0]="Emulated",t[t.None=2]="None",t[t.ShadowDom=3]="ShadowDom",t}({});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Et="undefined"!=typeof globalThis&&globalThis,At="undefined"!=typeof window&&window,kt="undefined"!=typeof self&&"undefined"!=typeof WorkerGlobalScope&&self instanceof WorkerGlobalScope&&self,xt="undefined"!=typeof global&&global,St=Et||xt||At||kt,Tt={},It=[],Vt=[],Ot=q({"\u0275cmp":q}),Dt=q({"\u0275dir":q}),Pt=q({"\u0275pipe":q}),jt=q({"\u0275mod":q}),Mt=q({"\u0275loc":q}),Nt=q({"\u0275fac":q}),Ht=q({__NG_ELEMENT_ID__:q});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let Rt=0;function Ft(t){return bt(()=>{const e={},n={type:t.type,providersResolver:null,decls:t.decls,vars:t.vars,factory:null,template:t.template||null,consts:t.consts||null,ngContentSelectors:t.ngContentSelectors,hostBindings:t.hostBindings||null,hostVars:t.hostVars||0,hostAttrs:t.hostAttrs||null,contentQueries:t.contentQueries||null,declaredInputs:e,inputs:null,outputs:null,exportAs:t.exportAs||null,onPush:t.changeDetection===wt.OnPush,directiveDefs:null,pipeDefs:null,selectors:t.selectors||Vt,viewQuery:t.viewQuery||null,features:t.features||null,data:t.data||{},encapsulation:t.encapsulation||Ct.Emulated,id:"c",styles:t.styles||Vt,_:null,setInput:null,schemas:t.schemas||null,tView:null},r=t.directives,s=t.features,o=t.pipes;return n.id+=Rt++,n.inputs=$t(t.inputs,e),n.outputs=$t(t.outputs),s&&s.forEach(t=>t(n)),n.directiveDefs=r?()=>("function"==typeof r?r():r).map(Lt):null,n.pipeDefs=o?()=>("function"==typeof o?o():o).map(Bt):null,n})}function Lt(t){return Gt(t)||function(t){return t[Dt]||null}(t)}function Bt(t){return function(t){return t[Pt]||null}(t)}const Ut={};function Zt(t){const e={type:t.type,bootstrap:t.bootstrap||Vt,declarations:t.declarations||Vt,imports:t.imports||Vt,exports:t.exports||Vt,transitiveCompileScopes:null,schemas:t.schemas||null,id:t.id||null};return null!=t.id&&bt(()=>{Ut[t.id]=t.type}),e}function $t(t,e){if(null==t)return Tt;const n={};for(const r in t)if(t.hasOwnProperty(r)){let s=t[r],o=s;Array.isArray(s)&&(o=s[1],s=s[0]),n[s]=r,e&&(e[s]=o)}return n}const zt=Ft;function Gt(t){return t[Ot]||null}function Wt(t,e){const n=t[jt]||null;if(!n&&!0===e)throw new Error(`Type ${K(t)} does not have '\u0275mod' property.`);return n}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Qt(t){return Array.isArray(t)&&"object"==typeof t[1]}function qt(t){return Array.isArray(t)&&!0===t[1]}function Jt(t){return 0!=(8&t.flags)}function Kt(t){return 2==(2&t.flags)}function Yt(t){return 1==(1&t.flags)}function Xt(t){return null!==t.template}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function te(t,e){return t.hasOwnProperty(Nt)?t[Nt]:null}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ee{constructor(t,e,n){this.previousValue=t,this.currentValue=e,this.firstChange=n}isFirstChange(){return this.firstChange}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function ne(){return re}function re(t){return t.type.prototype.ngOnChanges&&(t.setInput=oe),se}function se(){const t=ie(this),e=null==t?void 0:t.current;if(e){const n=t.previous;if(n===Tt)t.previous=e;else for(let t in e)n[t]=e[t];t.current=null,this.ngOnChanges(e)}}function oe(t,e,n,r){const s=ie(t)||function(t,e){return t.__ngSimpleChanges__=e}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */(t,{previous:Tt,current:null}),o=s.current||(s.current={}),i=s.previous,l=this.declaredInputs[n],u=i[l];o[l]=new ee(u&&u.currentValue,e,i===Tt),t[r]=e}function ie(t){return t.__ngSimpleChanges__||null}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let le;function ue(t){return!!t.listen}ne.ngInherit=!0;const ae={createRenderer:(t,e)=>void 0!==le?le:"undefined"!=typeof document?document:void 0};
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function ce(t){for(;Array.isArray(t);)t=t[0];return t}function he(t,e){return ce(e[t])}function de(t,e){return ce(e[t.index])}function fe(t,e){return t.data[e]}function pe(t,e){const n=e[t];return Qt(n)?n:n[0]}function _e(t){const e=function(t){return t.__ngContext__||null}(t);return e?Array.isArray(e)?e:e.lView:null}function ge(t){return 128==(128&t[2])}function ye(t,e){return null==e?null:t[e]}function me(t){t[18]=0}function ve(t,e){t[5]+=e;let n=t,r=t[3];for(;null!==r&&(1===e&&1===n[5]||-1===e&&0===n[5]);)r[5]+=e,n=r,r=r[3]}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const be={lFrame:Re(null),bindingsEnabled:!0,isInCheckNoChangesMode:!1};function we(){return be.bindingsEnabled}function Ce(){return be.lFrame.lView}function Ee(){return be.lFrame.tView}function Ae(){let t=ke();for(;null!==t&&64===t.type;)t=t.parent;return t}function ke(){return be.lFrame.currentTNode}function xe(t,e){const n=be.lFrame;n.currentTNode=t,n.isParent=e}function Se(){return be.lFrame.isParent}function Te(){return be.isInCheckNoChangesMode}function Ie(t){be.isInCheckNoChangesMode=t}function Ve(){return be.lFrame.bindingIndex++}function Oe(t,e){const n=be.lFrame;n.bindingIndex=n.bindingRootIndex=t,De(e)}function De(t){be.lFrame.currentDirectiveIndex=t}function Pe(t){be.lFrame.currentQueryIndex=t}function je(t){const e=t[1];return 2===e.type?e.declTNode:1===e.type?t[6]:null}function Me(t,e,n){if(n&gt.SkipSelf){let r=e,s=t;for(;r=r.parent,!(null!==r||n&gt.Host||(r=je(s),null===r)||(s=s[15],10&r.type)););if(null===r)return!1;e=r,t=s}const r=be.lFrame=He();return r.currentTNode=e,r.lView=t,!0}function Ne(t){const e=He(),n=t[1];be.lFrame=e,e.currentTNode=n.firstChild,e.lView=t,e.tView=n,e.contextLView=t,e.bindingIndex=n.bindingStartIndex,e.inI18n=!1}function He(){const t=be.lFrame,e=null===t?null:t.child;return null===e?Re(t):e}function Re(t){const e={currentTNode:null,isParent:!0,lView:null,tView:null,selectedIndex:-1,contextLView:null,elementDepthCount:0,currentNamespace:null,currentDirectiveIndex:-1,bindingRootIndex:-1,bindingIndex:-1,currentQueryIndex:0,parent:t,child:null,inI18n:!1};return null!==t&&(t.child=e),e}function Fe(){const t=be.lFrame;return be.lFrame=t.parent,t.currentTNode=null,t.lView=null,t}const Le=Fe;function Be(){const t=Fe();t.isParent=!0,t.tView=null,t.selectedIndex=-1,t.contextLView=null,t.elementDepthCount=0,t.currentDirectiveIndex=-1,t.currentNamespace=null,t.bindingRootIndex=-1,t.bindingIndex=-1,t.currentQueryIndex=0}function Ue(){return be.lFrame.selectedIndex}function Ze(t){be.lFrame.selectedIndex=t}function $e(t,e){for(let n=e.directiveStart,r=e.directiveEnd;n<r;n++){const e=t.data[n].type.prototype,{ngAfterContentInit:r,ngAfterContentChecked:s,ngAfterViewInit:o,ngAfterViewChecked:i,ngOnDestroy:l}=e;r&&(t.contentHooks||(t.contentHooks=[])).push(-n,r),s&&((t.contentHooks||(t.contentHooks=[])).push(n,s),(t.contentCheckHooks||(t.contentCheckHooks=[])).push(n,s)),o&&(t.viewHooks||(t.viewHooks=[])).push(-n,o),i&&((t.viewHooks||(t.viewHooks=[])).push(n,i),(t.viewCheckHooks||(t.viewCheckHooks=[])).push(n,i)),null!=l&&(t.destroyHooks||(t.destroyHooks=[])).push(n,l)}}function ze(t,e,n){Qe(t,e,3,n)}function Ge(t,e,n,r){(3&t[2])===n&&Qe(t,e,n,r)}function We(t,e){let n=t[2];(3&n)===e&&(n&=2047,n+=1,t[2]=n)}function Qe(t,e,n,r){const s=null!=r?r:-1,o=e.length-1;let i=0;for(let l=void 0!==r?65535&t[18]:0;l<o;l++)if("number"==typeof e[l+1]){if(i=e[l],null!=r&&i>=r)break}else e[l]<0&&(t[18]+=65536),(i<s||-1==s)&&(qe(t,n,e,l),t[18]=(4294901760&t[18])+l+2),l++}function qe(t,e,n,r){const s=n[r]<0,o=n[r+1],i=t[s?-n[r]:n[r]];if(s){if(t[2]>>11<t[18]>>16&&(3&t[2])===e){t[2]+=2048;try{o.call(i)}finally{}}}else try{o.call(i)}finally{}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Je{constructor(t,e,n){this.factory=t,this.resolving=!1,this.canSeeViewProviders=e,this.injectImpl=n}}function Ke(t,e,n){const r=ue(t);let s=0;for(;s<n.length;){const o=n[s];if("number"==typeof o){if(0!==o)break;s++;const i=n[s++],l=n[s++],u=n[s++];r?t.setAttribute(e,l,u,i):e.setAttributeNS(i,l,u)}else{const i=o,l=n[++s];Ye(i)?r&&t.setProperty(e,i,l):r?t.setAttribute(e,i,l):e.setAttribute(i,l),s++}}return s}function Ye(t){return 64===t.charCodeAt(0)}function Xe(t,e){if(null===e||0===e.length);else if(null===t||0===t.length)t=e.slice();else{let n=-1;for(let r=0;r<e.length;r++){const s=e[r];"number"==typeof s?n=s:0===n||tn(t,n,s,null,-1===n||2===n?e[++r]:null)}}return t}function tn(t,e,n,r,s){let o=0,i=t.length;if(-1===e)i=-1;else for(;o<t.length;){const n=t[o++];if("number"==typeof n){if(n===e){i=-1;break}if(n>e){i=o-1;break}}}for(;o<t.length;){const e=t[o];if("number"==typeof e)break;if(e===n){if(null===r)return void(null!==s&&(t[o+1]=s));if(r===t[o+1])return void(t[o+2]=s)}o++,null!==r&&o++,null!==s&&o++}-1!==i&&(t.splice(i,0,e),o=i+1),t.splice(o++,0,n),null!==r&&t.splice(o++,0,r),null!==s&&t.splice(o++,0,s)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function en(t){return 32767&t}function nn(t,e){let n=t>>16,r=e;for(;n>0;)r=r[15],n--;return r}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let rn=!0;function sn(t){const e=rn;return rn=t,e}let on=0;function ln(t,e){const n=an(t,e);if(-1!==n)return n;const r=e[1];r.firstCreatePass&&(t.injectorIndex=e.length,un(r.data,t),un(e,null),un(r.blueprint,null));const s=cn(t,e),o=t.injectorIndex;if(-1!==s){const t=en(s),n=nn(s,e),r=n[1].data;for(let s=0;s<8;s++)e[o+s]=n[t+s]|r[t+s]}return e[o+8]=s,o}function un(t,e){t.push(0,0,0,0,0,0,0,0,e)}function an(t,e){return-1===t.injectorIndex||t.parent&&t.parent.injectorIndex===t.injectorIndex||null===e[t.injectorIndex+8]?-1:t.injectorIndex}function cn(t,e){if(t.parent&&-1!==t.parent.injectorIndex)return t.parent.injectorIndex;let n=0,r=null,s=e;for(;null!==s;){const t=s[1],e=t.type;if(r=2===e?t.declTNode:1===e?s[6]:null,null===r)return-1;if(n++,s=s[15],-1!==r.injectorIndex)return r.injectorIndex|n<<16}return-1}function hn(t,e,n){!function(t,e,n){let r;"string"==typeof n?r=n.charCodeAt(0)||0:n.hasOwnProperty(Ht)&&(r=n[Ht]),null==r&&(r=n[Ht]=on++);const s=255&r;e.data[t+(s>>5)]|=1<<s}(t,e,n)}function dn(t,e,n){if(n&gt.Optional)return t;it(e,"NodeInjector")}function fn(t,e,n,r){if(n&gt.Optional&&void 0===r&&(r=null),0==(n&(gt.Self|gt.Host))){const s=t[9],o=mt(void 0);try{return s?s.get(e,r,n&gt.Optional):vt(e,r,n&gt.Optional)}finally{mt(o)}}return dn(r,e,n)}function pn(t,e,n,r=gt.Default,s){if(null!==t){const o=function(t){if("string"==typeof t)return t.charCodeAt(0)||0;const e=t.hasOwnProperty(Ht)?t[Ht]:void 0;return"number"==typeof e?e>=0?255&e:gn:e}(n);if("function"==typeof o){if(!Me(e,t,r))return r&gt.Host?dn(s,n,r):fn(e,n,r,s);try{const t=o();if(null!=t||r&gt.Optional)return t;it(n)}finally{Le()}}else if("number"==typeof o){let s=null,i=an(t,e),l=-1,u=r&gt.Host?e[16][6]:null;for((-1===i||r&gt.SkipSelf)&&(l=-1===i?cn(t,e):e[i+8],-1!==l&&bn(r,!1)?(s=e[1],i=en(l),e=nn(l,e)):i=-1);-1!==i;){const t=e[1];if(vn(o,i,t.data)){const t=yn(i,e,n,s,r,u);if(t!==_n)return t}l=e[i+8],-1!==l&&bn(r,e[1].data[i+8]===u)&&vn(o,i,e)?(s=t,i=en(l),e=nn(l,e)):i=-1}}}return fn(e,n,r,s)}const _n={};function gn(){return new wn(Ae(),Ce())}function yn(t,e,n,r,s,o){const i=e[1],l=i.data[t+8],u=function(t,e,n,r,s){const o=t.providerIndexes,i=e.data,l=1048575&o,u=t.directiveStart,a=o>>20,c=s?l+a:t.directiveEnd;for(let h=r?l:l+a;h<c;h++){const t=i[h];if(h<u&&n===t||h>=u&&t.type===n)return h}if(s){const t=i[u];if(t&&Xt(t)&&t.type===n)return u}return null}(l,i,n,null==r?Kt(l)&&rn:r!=i&&0!=(3&l.type),s&gt.Host&&o===l);return null!==u?mn(e,i,u,l):_n}function mn(t,e,n,r){let s=t[n];const o=e.data;if(s instanceof Je){const i=s;i.resolving&&function(t,e){throw new rt("200",`Circular dependency in DI detected for ${t}`)}(ot(o[n]));const l=sn(i.canSeeViewProviders);i.resolving=!0;const u=i.injectImpl?mt(i.injectImpl):null;Me(t,r,gt.Default);try{s=t[n]=i.factory(void 0,o,t,r),e.firstCreatePass&&n>=r.directiveStart&&
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
function(t,e,n){const{ngOnChanges:r,ngOnInit:s,ngDoCheck:o}=e.type.prototype;if(r){const r=re(e);(n.preOrderHooks||(n.preOrderHooks=[])).push(t,r),(n.preOrderCheckHooks||(n.preOrderCheckHooks=[])).push(t,r)}s&&(n.preOrderHooks||(n.preOrderHooks=[])).push(0-t,s),o&&((n.preOrderHooks||(n.preOrderHooks=[])).push(t,o),(n.preOrderCheckHooks||(n.preOrderCheckHooks=[])).push(t,o))}(n,o[n],e)}finally{null!==u&&mt(u),sn(l),i.resolving=!1,Le()}}return s}function vn(t,e,n){return!!(n[e+(t>>5)]&1<<t)}function bn(t,e){return!(t&gt.Self||t&gt.Host&&e)}class wn{constructor(t,e){this._tNode=t,this._lView=e}get(t,e){return pn(this._tNode,this._lView,t,void 0,e)}}function Cn(t){return bt(()=>{const e=t.prototype.constructor,n=e[Nt]||En(e),r=Object.prototype;let s=Object.getPrototypeOf(t.prototype).constructor;for(;s&&s!==r;){const t=s[Nt]||En(s);if(t&&t!==n)return t;s=Object.getPrototypeOf(s)}return t=>new t})}function En(t){return nt(t)?()=>{const e=En(et(t));return e&&e()}:te(t)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function An(t,e,n){return bt(()=>{const r=function(t){return function(...e){if(t){const n=t(...e);for(const t in n)this[t]=n[t]}}}(e);function s(...t){if(this instanceof s)return r.apply(this,t),this;const e=new s(...t);return n.annotation=e,n;function n(t,n,r){const s=t.hasOwnProperty("__parameters__")?t.__parameters__:Object.defineProperty(t,"__parameters__",{value:[]}).__parameters__;for(;s.length<=r;)s.push(null);return(s[r]=s[r]||[]).push(e),t}}return n&&(s.prototype=Object.create(n.prototype)),s.prototype.ngMetadataName=t,s.annotationCls=s,s})}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class kn{constructor(t,e){this._desc=t,this.ngMetadataName="InjectionToken",this.\u0275prov=void 0,"number"==typeof e?this.__NG_ELEMENT_ID__=e:void 0!==e&&(this.\u0275prov=lt({token:this,providedIn:e.providedIn||"root",factory:e.factory}))}toString(){return`InjectionToken ${this._desc}`}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function xn(t,e){t.forEach(t=>Array.isArray(t)?xn(t,e):e(t))}function Sn(t,e){return e>=t.length-1?t.pop():t.splice(e,1)[0]}function Tn(t,e,n){let r=Vn(t,e);return r>=0?t[1|r]=n:(r=~r,function(t,e,n,r){let s=t.length;if(s==e)t.push(n,r);else if(1===s)t.push(r,t[0]),t[0]=n;else{for(s--,t.push(t[s-1],t[s]);s>e;)t[s]=t[s-2],s--;t[e]=n,t[e+1]=r}}(t,r,e,n)),r}function In(t,e){const n=Vn(t,e);if(n>=0)return t[1|n]}function Vn(t,e){return function(t,e,n){let r=0,s=t.length>>1;for(;s!==r;){const n=r+(s-r>>1),o=t[n<<1];if(e===o)return n<<1;o>e?s=n:r=n+1}return~(s<<1)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */(t,e)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const On={},Dn=/\n/gm,Pn=q({provide:String,useValue:q});let jn;function Mn(t){const e=jn;return jn=t,e}function Nn(t,e=gt.Default){if(void 0===jn)throw new Error("inject() must be called from an injection context");return null===jn?vt(t,void 0,e):jn.get(t,e&gt.Optional?null:void 0,e)}function Hn(t,e=gt.Default){return(yt||Nn)(et(t),e)}function Rn(t){const e=[];for(let n=0;n<t.length;n++){const r=et(t[n]);if(Array.isArray(r)){if(0===r.length)throw new Error("Arguments array must have arguments.");let t,n=gt.Default;for(let e=0;e<r.length;e++){const s=r[e],o=s.__NG_DI_FLAG__;"number"==typeof o?-1===o?t=s.token:n|=o:t=s}e.push(Hn(t,n))}else e.push(Hn(r))}return e}function Fn(t,e){return t.__NG_DI_FLAG__=e,t.prototype.__NG_DI_FLAG__=e,t}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Ln=Fn(An("Inject",t=>({token:t})),-1),Bn=Fn(An("Optional"),8),Un=Fn(An("SkipSelf"),4);function Zn(t){return t.ngDebugContext}function $n(t){return t.ngOriginalError}function zn(t,...e){t.error(...e)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Gn{constructor(){this._console=console}handleError(t){const e=this._findOriginalError(t),n=this._findContext(t),r=function(t){return t.ngErrorLogger||zn}(t);r(this._console,"ERROR",t),e&&r(this._console,"ORIGINAL ERROR",e),n&&r(this._console,"ERROR CONTEXT",n)}_findContext(t){return t?Zn(t)?Zn(t):this._findContext($n(t)):null}_findOriginalError(t){let e=$n(t);for(;e&&$n(e);)e=$n(e);return e}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Wn(t,e){t.__ngContext__=e}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Qn=(()=>("undefined"!=typeof requestAnimationFrame&&requestAnimationFrame||setTimeout).bind(St))();function qn(t){return t instanceof Function?t():t}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */var Jn=function(t){return t[t.Important=1]="Important",t[t.DashCase=2]="DashCase",t}({});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Kn(t,e){return(void 0)(t,e)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Yn(t){const e=t[3];return qt(e)?e[3]:e}function Xn(t){return er(t[13])}function tr(t){return er(t[4])}function er(t){for(;null!==t&&!qt(t);)t=t[4];return t}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function nr(t,e,n,r,s){if(null!=r){let o,i=!1;qt(r)?o=r:Qt(r)&&(i=!0,r=r[0]);const l=ce(r);0===t&&null!==n?null==s?lr(e,n,l):ir(e,n,l,s||null,!0):1===t&&null!==n?ir(e,n,l,s||null,!0):2===t?function(t,e,n){const r=function(t,e){return ue(t)?t.parentNode(e):e.parentNode}(t,e);r&&function(t,e,n,r){ue(t)?t.removeChild(e,n,r):e.removeChild(n)}(t,r,e,n)}(e,l,i):3===t&&e.destroyNode(l),null!=o&&function(t,e,n,r,s){const o=n[7];o!==ce(n)&&nr(e,t,r,o,s);for(let i=10;i<n.length;i++){const s=n[i];dr(s[1],s,t,e,r,o)}}(e,t,o,n,s)}}function rr(t,e,n){return ue(t)?t.createElement(e,n):null===n?t.createElement(e):t.createElementNS(n,e)}function sr(t,e){const n=t[9],r=n.indexOf(e),s=e[3];1024&e[2]&&(e[2]&=-1025,ve(s,-1)),n.splice(r,1)}function or(t,e){if(!(256&e[2])){e[2]&=-129,e[2]|=256,function(t,e){let n;if(null!=t&&null!=(n=t.destroyHooks))for(let r=0;r<n.length;r+=2){const t=e[n[r]];if(!(t instanceof Je)){const e=n[r+1];if(Array.isArray(e))for(let n=0;n<e.length;n+=2)e[n+1].call(t[e[n]]);else e.call(t)}}}(t,e),function(t,e){const n=t.cleanup,r=e[7];let s=-1;if(null!==n)for(let o=0;o<n.length-1;o+=2)if("string"==typeof n[o]){const t=n[o+1],i="function"==typeof t?t(e):ce(e[t]),l=r[s=n[o+2]],u=n[o+3];"boolean"==typeof u?i.removeEventListener(n[o],l,u):u>=0?r[s=u]():r[s=-u].unsubscribe(),o+=2}else{const t=r[s=n[o+1]];n[o].call(t)}if(null!==r){for(let t=s+1;t<r.length;t++)(0,r[t])();e[7]=null}}(t,e),1===e[1].type&&ue(e[11])&&e[11].destroy();const n=e[17];if(null!==n&&qt(e[3])){n!==e[3]&&sr(n,e);const r=e[19];null!==r&&r.detachView(t)}}}function ir(t,e,n,r,s){ue(t)?t.insertBefore(e,n,r,s):e.insertBefore(n,r,s)}function lr(t,e,n){ue(t)?t.appendChild(e,n):e.appendChild(n)}function ur(t,e,n,r,s){null!==r?ir(t,e,n,r,s):lr(t,e,n)}function ar(t,e,n,r){const s=function(t,e,n){return function(t,e,n){let r=e;for(;null!==r&&40&r.type;)r=(e=r).parent;if(null===r)return n[0];if(2&r.flags){const e=t.data[r.directiveStart].encapsulation;if(e===Ct.None||e===Ct.Emulated)return null}return de(r,n)}(t,e.parent,n)}(t,r,e),o=e[11],i=function(t,e,n){return function(t,e,n){return 40&t.type?de(t,n):null}(t,0,n)}(r.parent||e[6],0,e);if(null!=s)if(Array.isArray(n))for(let l=0;l<n.length;l++)ur(o,s,n[l],i,!1);else ur(o,s,n,i,!1)}function cr(t,e){return null!==e?t[16][6].projection[e.projection]:null}function hr(t,e,n,r,s,o,i){for(;null!=n;){const l=r[n.index],u=n.type;if(i&&0===e&&(l&&Wn(ce(l),r),n.flags|=4),64!=(64&n.flags))if(8&u)hr(t,e,n.child,r,s,o,!1),nr(e,t,s,l,o);else if(32&u){const i=Kn(n,r);let u;for(;u=i();)nr(e,t,s,u,o);nr(e,t,s,l,o)}else 16&u?fr(t,e,r,n,s,o):nr(e,t,s,l,o);n=i?n.projectionNext:n.next}}function dr(t,e,n,r,s,o){hr(n,r,t.firstChild,e,s,o,!1)}function fr(t,e,n,r,s,o){const i=n[16],l=i[6].projection[r.projection];if(Array.isArray(l))for(let u=0;u<l.length;u++)nr(e,t,s,l[u],o);else hr(t,e,l,i[3],s,o,!0)}function pr(t,e,n){ue(t)?t.setAttribute(e,"style",n):e.style.cssText=n}function _r(t,e,n){ue(t)?""===n?t.removeAttribute(e,"class"):t.setAttribute(e,"class",n):e.className=n}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function gr(t,e,n){let r=t.length;for(;;){const s=t.indexOf(e,n);if(-1===s)return s;if(0===s||t.charCodeAt(s-1)<=32){const n=e.length;if(s+n===r||t.charCodeAt(s+n)<=32)return s}n=s+1}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function yr(t,e,n){let r=0;for(;r<t.length;){let s=t[r++];if(n&&"class"===s){if(s=t[r],-1!==gr(s.toLowerCase(),e,0))return!0}else if(1===s){for(;r<t.length&&"string"==typeof(s=t[r++]);)if(s.toLowerCase()===e)return!0;return!1}}return!1}function mr(t){return 4===t.type&&"ng-template"!==t.value}function vr(t,e,n){return e===(4!==t.type||n?t.value:"ng-template")}function br(t,e,n){let r=4;const s=t.attrs||[],o=function(t){for(let n=0;n<t.length;n++)if(3===(e=t[n])||4===e||6===e)return n;var e;return t.length}(s);let i=!1;for(let l=0;l<e.length;l++){const u=e[l];if("number"!=typeof u){if(!i)if(4&r){if(r=2|1&r,""!==u&&!vr(t,u,n)||""===u&&1===e.length){if(wr(r))return!1;i=!0}}else{const a=8&r?u:e[++l];if(8&r&&null!==t.attrs){if(!yr(t.attrs,a,n)){if(wr(r))return!1;i=!0}continue}const c=Cr(8&r?"class":u,s,mr(t),n);if(-1===c){if(wr(r))return!1;i=!0;continue}if(""!==a){let t;t=c>o?"":s[c+1].toLowerCase();const e=8&r?t:null;if(e&&-1!==gr(e,a,0)||2&r&&a!==t){if(wr(r))return!1;i=!0}}}}else{if(!i&&!wr(r)&&!wr(u))return!1;if(i&&wr(u))continue;i=!1,r=u|1&r}}return wr(r)||i}function wr(t){return 0==(1&t)}function Cr(t,e,n,r){if(null===e)return-1;let s=0;if(r||!n){let n=!1;for(;s<e.length;){const r=e[s];if(r===t)return s;if(3===r||6===r)n=!0;else{if(1===r||2===r){let t=e[++s];for(;"string"==typeof t;)t=e[++s];continue}if(4===r)break;if(0===r){s+=4;continue}}s+=n?1:2}return-1}return function(t,e){let n=t.indexOf(4);if(n>-1)for(n++;n<t.length;){const r=t[n];if("number"==typeof r)return-1;if(r===e)return n;n++}return-1}(e,t)}function Er(t,e,n=!1){for(let r=0;r<e.length;r++)if(br(t,e[r],n))return!0;return!1}function Ar(t,e){return t?":not("+e.trim()+")":e}function kr(t){let e=t[0],n=1,r=2,s="",o=!1;for(;n<t.length;){let i=t[n];if("string"==typeof i)if(2&r){const e=t[++n];s+="["+i+(e.length>0?'="'+e+'"':"")+"]"}else 8&r?s+="."+i:4&r&&(s+=" "+i);else""===s||wr(i)||(e+=Ar(o,s),s=""),r=i,o=o||!wr(r);n++}return""!==s&&(e+=Ar(o,s)),e}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const xr={};
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Sr(t){Tr(Ee(),Ce(),Ue()+t,Te())}function Tr(t,e,n,r){if(!r)if(3==(3&e[2])){const r=t.preOrderCheckHooks;null!==r&&ze(e,r,n)}else{const r=t.preOrderHooks;null!==r&&Ge(e,r,0,n)}Ze(n)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Ir(t,e){return t<<17|e<<2}function Vr(t){return t>>17&32767}function Or(t){return 2|t}function Dr(t){return(131068&t)>>2}function Pr(t,e){return-131069&t|e<<2}function jr(t){return 1|t}function Mr(t,e){const n=t.contentQueries;if(null!==n)for(let r=0;r<n.length;r+=2){const s=n[r],o=n[r+1];if(-1!==o){const n=t.data[o];Pe(s),n.contentQueries(2,e[o],o)}}}function Nr(t,e,n,r,s,o,i,l,u,a){const c=e.blueprint.slice();return c[0]=s,c[2]=140|r,me(c),c[3]=c[15]=t,c[8]=n,c[10]=i||t&&t[10],c[11]=l||t&&t[11],c[12]=u||t&&t[12]||null,c[9]=a||t&&t[9]||null,c[6]=o,c[16]=2==e.type?t[16]:c,c}function Hr(t,e,n,r,s){let o=t.data[e];if(null===o)o=function(t,e,n,r,s){const o=ke(),i=Se(),l=t.data[e]=function(t,e,n,r,s,o){return{type:n,index:r,insertBeforeIndex:null,injectorIndex:e?e.injectorIndex:-1,directiveStart:-1,directiveEnd:-1,directiveStylingLast:-1,propertyBindings:null,flags:0,providerIndexes:0,value:s,attrs:o,mergedAttrs:null,localNames:null,initialInputs:void 0,inputs:null,outputs:null,tViews:null,next:null,projectionNext:null,child:null,parent:e,projection:null,styles:null,stylesWithoutHost:null,residualStyles:void 0,classes:null,classesWithoutHost:null,residualClasses:void 0,classBindings:0,styleBindings:0}}(0,i?o:o&&o.parent,n,e,r,s);return null===t.firstChild&&(t.firstChild=l),null!==o&&(i?null==o.child&&null!==l.parent&&(o.child=l):null===o.next&&(o.next=l)),l}(t,e,n,r,s),be.lFrame.inI18n&&(o.flags|=64);else if(64&o.type){o.type=n,o.value=r,o.attrs=s;const t=function(){const t=be.lFrame,e=t.currentTNode;return t.isParent?e:e.parent}();o.injectorIndex=null===t?-1:t.injectorIndex}return xe(o,!0),o}function Rr(t,e,n,r){if(0===n)return-1;const s=e.length;for(let o=0;o<n;o++)e.push(r),t.blueprint.push(r),t.data.push(null);return s}function Fr(t,e,n){Ne(e);try{const r=t.viewQuery;null!==r&&us(1,r,n);const s=t.template;null!==s&&Ur(t,e,s,1,n),t.firstCreatePass&&(t.firstCreatePass=!1),t.staticContentQueries&&Mr(t,e),t.staticViewQueries&&us(2,t.viewQuery,n);const o=t.components;null!==o&&function(t,e){for(let n=0;n<e.length;n++)rs(t,e[n])}(e,o)}catch(r){throw t.firstCreatePass&&(t.incompleteFirstPass=!0),r}finally{e[2]&=-5,Be()}}function Lr(t,e,n,r){const s=e[2];if(256==(256&s))return;Ne(e);const o=Te();try{me(e),be.lFrame.bindingIndex=t.bindingStartIndex,null!==n&&Ur(t,e,n,2,r);const i=3==(3&s);if(!o)if(i){const n=t.preOrderCheckHooks;null!==n&&ze(e,n,null)}else{const n=t.preOrderHooks;null!==n&&Ge(e,n,0,null),We(e,0)}if(function(t){for(let e=Xn(t);null!==e;e=tr(e)){if(!e[2])continue;const t=e[9];for(let e=0;e<t.length;e++){const n=t[e],r=n[3];0==(1024&n[2])&&ve(r,1),n[2]|=1024}}}(e),function(t){for(let e=Xn(t);null!==e;e=tr(e))for(let t=10;t<e.length;t++){const n=e[t],r=n[1];ge(n)&&Lr(r,n,r.template,n[8])}}(e),null!==t.contentQueries&&Mr(t,e),!o)if(i){const n=t.contentCheckHooks;null!==n&&ze(e,n)}else{const n=t.contentHooks;null!==n&&Ge(e,n,1),We(e,1)}!function(t,e){const n=t.hostBindingOpCodes;if(null!==n)try{for(let t=0;t<n.length;t++){const r=n[t];if(r<0)Ze(~r);else{const s=r,o=n[++t],i=n[++t];Oe(o,s),i(2,e[s])}}}finally{Ze(-1)}}(t,e);const l=t.components;null!==l&&function(t,e){for(let n=0;n<e.length;n++)es(t,e[n])}(e,l);const u=t.viewQuery;if(null!==u&&us(2,u,r),!o)if(i){const n=t.viewCheckHooks;null!==n&&ze(e,n)}else{const n=t.viewHooks;null!==n&&Ge(e,n,2),We(e,2)}!0===t.firstUpdatePass&&(t.firstUpdatePass=!1),o||(e[2]&=-73),1024&e[2]&&(e[2]&=-1025,ve(e[3],-1))}finally{Be()}}function Br(t,e,n,r){const s=e[10],o=!Te(),i=4==(4&e[2]);try{o&&!i&&s.begin&&s.begin(),i&&Fr(t,e,r),Lr(t,e,n,r)}finally{o&&!i&&s.end&&s.end()}}function Ur(t,e,n,r,s){const o=Ue(),i=2&r;try{Ze(-1),i&&e.length>20&&Tr(t,e,20,Te()),n(r,s)}finally{Ze(o)}}function Zr(t){const e=t.tView;return null===e||e.incompleteFirstPass?t.tView=$r(1,null,t.template,t.decls,t.vars,t.directiveDefs,t.pipeDefs,t.viewQuery,t.schemas,t.consts):e}function $r(t,e,n,r,s,o,i,l,u,a){const c=20+r,h=c+s,d=function(t,e){const n=[];for(let r=0;r<e;r++)n.push(r<t?null:xr);return n}(c,h),f="function"==typeof a?a():a;return d[1]={type:t,blueprint:d,template:n,queries:null,viewQuery:l,declTNode:e,data:d.slice().fill(null,c),bindingStartIndex:c,expandoStartIndex:h,hostBindingOpCodes:null,firstCreatePass:!0,firstUpdatePass:!0,staticViewQueries:!1,staticContentQueries:!1,preOrderHooks:null,preOrderCheckHooks:null,contentHooks:null,contentCheckHooks:null,viewHooks:null,viewCheckHooks:null,destroyHooks:null,cleanup:null,contentQueries:null,components:null,directiveRegistry:"function"==typeof o?o():o,pipeRegistry:"function"==typeof i?i():i,firstChild:null,schemas:u,consts:f,incompleteFirstPass:!1}}function zr(t,e,n){for(let r in t)if(t.hasOwnProperty(r)){const s=t[r];(n=null===n?{}:n).hasOwnProperty(r)?n[r].push(e,s):n[r]=[e,s]}return n}function Gr(t,e,n,r,s,o){const i=o.hostBindings;if(i){let n=t.hostBindingOpCodes;null===n&&(n=t.hostBindingOpCodes=[]);const o=~e.index;(function(t){let e=t.length;for(;e>0;){const n=t[--e];if("number"==typeof n&&n<0)return n}return 0})(n)!=o&&n.push(o),n.push(r,s,i)}}function Wr(t,e){null!==t.hostBindings&&t.hostBindings(1,e)}function Qr(t,e){e.flags|=2,(t.components||(t.components=[])).push(e.index)}function qr(t,e,n){if(n){if(e.exportAs)for(let r=0;r<e.exportAs.length;r++)n[e.exportAs[r]]=t;Xt(e)&&(n[""]=t)}}function Jr(t,e,n){t.flags|=1,t.directiveStart=e,t.directiveEnd=e+n,t.providerIndexes=e}function Kr(t,e,n,r,s){t.data[r]=s;const o=s.factory||(s.factory=te(s.type)),i=new Je(o,Xt(s),null);t.blueprint[r]=i,n[r]=i,Gr(t,e,0,r,Rr(t,n,s.hostVars,xr),s)}function Yr(t,e,n){const r=de(e,t),s=Zr(n),o=t[10],i=ss(t,Nr(t,s,null,n.onPush?64:16,r,e,o,o.createRenderer(r,n),null,null));t[e.index]=i}function Xr(t,e,n,r,s,o){const i=o[e];if(null!==i){const t=r.setInput;for(let e=0;e<i.length;){const s=i[e++],o=i[e++],l=i[e++];null!==t?r.setInput(n,l,s,o):n[o]=l}}}function ts(t,e){let n=null,r=0;for(;r<e.length;){const s=e[r];if(0!==s)if(5!==s){if("number"==typeof s)break;t.hasOwnProperty(s)&&(null===n&&(n=[]),n.push(s,t[s],e[r+1])),r+=2}else r+=2;else r+=4}return n}function es(t,e){const n=pe(e,t);if(ge(n)){const t=n[1];80&n[2]?Lr(t,n,t.template,n[8]):n[5]>0&&ns(n)}}function ns(t){for(let n=Xn(t);null!==n;n=tr(n))for(let t=10;t<n.length;t++){const e=n[t];if(1024&e[2]){const t=e[1];Lr(t,e,t.template,e[8])}else e[5]>0&&ns(e)}const e=t[1].components;if(null!==e)for(let n=0;n<e.length;n++){const r=pe(e[n],t);ge(r)&&r[5]>0&&ns(r)}}function rs(t,e){const n=pe(e,t),r=n[1];!function(t,e){for(let n=e.length;n<t.blueprint.length;n++)e.push(t.blueprint[n])}(r,n),Fr(r,n,n[8])}function ss(t,e){return t[13]?t[14][4]=e:t[13]=e,t[14]=e,e}function os(t){for(;t;){t[2]|=64;const e=Yn(t);if(0!=(512&t[2])&&!e)return t;t=e}return null}function is(t,e,n){const r=e[10];r.begin&&r.begin();try{Lr(t,e,t.template,n)}catch(s){throw ds(e,s),s}finally{r.end&&r.end()}}function ls(t){!function(t){for(let e=0;e<t.components.length;e++){const n=t.components[e],r=_e(n),s=r[1];Br(s,r,s.template,n)}}(t[8])}function us(t,e,n){Pe(0),e(t,n)}const as=(()=>Promise.resolve(null))();function cs(t){return t[7]||(t[7]=[])}function hs(t){return t.cleanup||(t.cleanup=[])}function ds(t,e){const n=t[9],r=n?n.get(Gn,null):null;r&&r.handleError(e)}function fs(t,e,n,r,s){for(let o=0;o<n.length;){const i=n[o++],l=n[o++],u=e[i],a=t.data[i];null!==a.setInput?a.setInput(u,s,r,l):u[l]=s}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function ps(t,e,n){let r=n?t.styles:null,s=n?t.classes:null,o=0;if(null!==e)for(let i=0;i<e.length;i++){const t=e[i];"number"==typeof t?o=t:1==o?s=Y(s,t):2==o&&(r=Y(r,t+": "+e[++i]+";"))}n?t.styles=r:t.stylesWithoutHost=r,n?t.classes=s:t.classesWithoutHost=s}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const _s=new kn("INJECTOR",-1);
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class gs{get(t,e=On){if(e===On){const e=new Error(`NullInjectorError: No provider for ${K(t)}!`);throw e.name="NullInjectorError",e}return e}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const ys=new kn("Set Injector scope."),ms={},vs={},bs=[];
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let ws;function Cs(){return void 0===ws&&(ws=new gs),ws}function Es(t,e=null,n=null,r){return new As(t,n,e||Cs(),r)}class As{constructor(t,e,n,r=null){this.parent=n,this.records=new Map,this.injectorDefTypes=new Set,this.onDestroy=new Set,this._destroyed=!1;const s=[];e&&xn(e,n=>this.processProvider(n,t,e)),xn([t],t=>this.processInjectorType(t,[],s)),this.records.set(_s,Ss(void 0,this));const o=this.records.get(ys);this.scope=null!=o?o.value:null,this.source=r||("object"==typeof t?null:K(t))}get destroyed(){return this._destroyed}destroy(){this.assertNotDestroyed(),this._destroyed=!0;try{this.onDestroy.forEach(t=>t.ngOnDestroy())}finally{this.records.clear(),this.onDestroy.clear(),this.injectorDefTypes.clear()}}get(t,e=On,n=gt.Default){this.assertNotDestroyed();const r=Mn(this);try{if(!(n&gt.SkipSelf)){let e=this.records.get(t);if(void 0===e){const n=("function"==typeof(s=t)||"object"==typeof s&&s instanceof kn)&&at(t);e=n&&this.injectableDefInScope(n)?Ss(ks(t),ms):null,this.records.set(t,e)}if(null!=e)return this.hydrate(t,e)}return(n&gt.Self?Cs():this.parent).get(t,e=n&gt.Optional&&e===On?null:e)}catch(o){if("NullInjectorError"===o.name){if((o.ngTempTokenPath=o.ngTempTokenPath||[]).unshift(K(t)),r)throw o;return function(t,e,n,r){const s=t.ngTempTokenPath;throw e.__source&&s.unshift(e.__source),t.message=function(t,e,n,r=null){t=t&&"\n"===t.charAt(0)&&"\u0275"==t.charAt(1)?t.substr(2):t;let s=K(e);if(Array.isArray(e))s=e.map(K).join(" -> ");else if("object"==typeof e){let t=[];for(let n in e)if(e.hasOwnProperty(n)){let r=e[n];t.push(n+":"+("string"==typeof r?JSON.stringify(r):K(r)))}s=`{${t.join(", ")}}`}return`${n}${r?"("+r+")":""}[${s}]: ${t.replace(Dn,"\n  ")}`}("\n"+t.message,s,n,r),t.ngTokenPath=s,t.ngTempTokenPath=null,t}(o,t,"R3InjectorError",this.source)}throw o}finally{Mn(r)}var s;
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}_resolveInjectorDefTypes(){this.injectorDefTypes.forEach(t=>this.get(t))}toString(){const t=[];return this.records.forEach((e,n)=>t.push(K(n))),`R3Injector[${t.join(", ")}]`}assertNotDestroyed(){if(this._destroyed)throw new Error("Injector has already been destroyed.")}processInjectorType(t,e,n){if(!(t=et(t)))return!1;let r=ht(t);const s=null==r&&t.ngModule||void 0,o=void 0===s?t:s,i=-1!==n.indexOf(o);if(void 0!==s&&(r=ht(s)),null==r)return!1;if(null!=r.imports&&!i){let t;n.push(o);try{xn(r.imports,r=>{this.processInjectorType(r,e,n)&&(void 0===t&&(t=[]),t.push(r))})}finally{}if(void 0!==t)for(let e=0;e<t.length;e++){const{ngModule:n,providers:r}=t[e];xn(r,t=>this.processProvider(t,n,r||bs))}}this.injectorDefTypes.add(o);const l=te(o)||(()=>new o);this.records.set(o,Ss(l,ms));const u=r.providers;if(null!=u&&!i){const e=t;xn(u,t=>this.processProvider(t,e,u))}return void 0!==s&&void 0!==t.providers}processProvider(t,e,n){let r=Is(t=et(t))?t:et(t&&t.provide);const s=function(t,e,n){return Ts(t)?Ss(void 0,t.useValue):Ss(xs(t),ms)}(t);if(Is(t)||!0!==t.multi)this.records.get(r);else{let e=this.records.get(r);e||(e=Ss(void 0,ms,!0),e.factory=()=>Rn(e.multi),this.records.set(r,e)),r=t,e.multi.push(t)}this.records.set(r,s)}hydrate(t,e){var n;return e.value===ms&&(e.value=vs,e.value=e.factory()),"object"==typeof e.value&&e.value&&null!==(n=e.value)&&"object"==typeof n&&"function"==typeof n.ngOnDestroy&&this.onDestroy.add(e.value),e.value}injectableDefInScope(t){return!!t.providedIn&&("string"==typeof t.providedIn?"any"===t.providedIn||t.providedIn===this.scope:this.injectorDefTypes.has(t.providedIn))}}function ks(t){const e=at(t),n=null!==e?e.factory:te(t);if(null!==n)return n;if(t instanceof kn)throw new Error(`Token ${K(t)} is missing a \u0275prov definition.`);if(t instanceof Function)return function(t){const e=t.length;if(e>0){const n=function(t,e){const n=[];for(let r=0;r<t;r++)n.push("?");return n}(e);throw new Error(`Can't resolve all parameters for ${K(t)}: (${n.join(", ")}).`)}const n=function(t){const e=t&&(t[dt]||t[pt]);if(e){const n=function(t){if(t.hasOwnProperty("name"))return t.name;const e=(""+t).match(/^function\s*([^\s(]+)/);return null===e?"":e[1]}(t);return console.warn(`DEPRECATED: DI is instantiating a token "${n}" that inherits its @Injectable decorator but does not provide one itself.\nThis will become an error in a future version of Angular. Please add @Injectable() to the "${n}" class.`),e}return null}(t);return null!==n?()=>n.factory(t):()=>new t}(t);throw new Error("unreachable")}function xs(t,e,n){let r;if(Is(t)){const e=et(t);return te(e)||ks(e)}if(Ts(t))r=()=>et(t.useValue);else if((s=t)&&s.useFactory)r=()=>t.useFactory(...Rn(t.deps||[]));else if(function(t){return!(!t||!t.useExisting)}(t))r=()=>Hn(et(t.useExisting));else{const e=et(t&&(t.useClass||t.provide));if(!function(t){return!!t.deps}(t))return te(e)||ks(e);r=()=>new e(...Rn(t.deps))}var s;return r}function Ss(t,e,n=!1){return{factory:t,value:e,multi:n?[]:void 0}}function Ts(t){return null!==t&&"object"==typeof t&&Pn in t}function Is(t){return"function"==typeof t}const Vs=function(t,e,n){return function(t,e=null,n=null,r){const s=Es(t,e,n,r);return s._resolveInjectorDefTypes(),s}({name:n},e,t,n)};let Os=(()=>{class t{static create(t,e){return Array.isArray(t)?Vs(t,e,""):Vs(t.providers,t.parent,t.name||"")}}return t.THROW_IF_NOT_FOUND=On,t.NULL=new gs,t.\u0275prov=lt({token:t,providedIn:"any",factory:()=>Hn(_s)}),t.__NG_ELEMENT_ID__=-1,t})();function Ds(t,e){$e(_e(t)[1],Ae())}function Ps(t){let e=Object.getPrototypeOf(t.type.prototype).constructor,n=!0;const r=[t];for(;e;){let s;if(Xt(t))s=e.\u0275cmp||e.\u0275dir;else{if(e.\u0275cmp)throw new Error("Directives cannot inherit Components");s=e.\u0275dir}if(s){if(n){r.push(s);const e=t;e.inputs=js(t.inputs),e.declaredInputs=js(t.declaredInputs),e.outputs=js(t.outputs);const n=s.hostBindings;n&&Hs(t,n);const o=s.viewQuery,i=s.contentQueries;if(o&&Ms(t,o),i&&Ns(t,i),J(t.inputs,s.inputs),J(t.declaredInputs,s.declaredInputs),J(t.outputs,s.outputs),Xt(s)&&s.data.animation){const e=t.data;e.animation=(e.animation||[]).concat(s.data.animation)}}const e=s.features;if(e)for(let r=0;r<e.length;r++){const s=e[r];s&&s.ngInherit&&s(t),s===Ps&&(n=!1)}}e=Object.getPrototypeOf(e)}!function(t){let e=0,n=null;for(let r=t.length-1;r>=0;r--){const s=t[r];s.hostVars=e+=s.hostVars,s.hostAttrs=Xe(s.hostAttrs,n=Xe(n,s.hostAttrs))}}(r)}function js(t){return t===Tt?{}:t===Vt?[]:t}function Ms(t,e){const n=t.viewQuery;t.viewQuery=n?(t,r)=>{e(t,r),n(t,r)}:e}function Ns(t,e){const n=t.contentQueries;t.contentQueries=n?(t,r,s)=>{e(t,r,s),n(t,r,s)}:e}function Hs(t,e){const n=t.hostBindings;t.hostBindings=n?(t,r)=>{e(t,r),n(t,r)}:e}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let Rs=null;function Fs(){if(!Rs){const t=St.Symbol;if(t&&t.iterator)Rs=t.iterator;else{const t=Object.getOwnPropertyNames(Map.prototype);for(let e=0;e<t.length;++e){const n=t[e];"entries"!==n&&"size"!==n&&Map.prototype[n]===Map.prototype.entries&&(Rs=n)}}}return Rs}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Ls(t){return!!Bs(t)&&(Array.isArray(t)||!(t instanceof Map)&&Fs()in t)}function Bs(t){return null!==t&&("function"==typeof t||"object"==typeof t)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Us(t,e,n){return!Object.is(t[e],n)&&(t[e]=n,!0)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Zs(t,e=gt.Default){const n=Ce();return null===n?Hn(t,e):pn(Ae(),n,et(t),e)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function $s(t,e,n){const r=Ce();return Us(r,Ve(),e)&&function(t,e,n,r,s,o,i,l){const u=de(e,n);let a,c=e.inputs;var h;null!=c&&(a=c[r])?(fs(t,n,a,r,s),Kt(e)&&function(t,e){const n=pe(e,t);16&n[2]||(n[2]|=64)}(n,e.index)):3&e.type&&(r="class"===(h=r)?"className":"for"===h?"htmlFor":"formaction"===h?"formAction":"innerHtml"===h?"innerHTML":"readonly"===h?"readOnly":"tabindex"===h?"tabIndex":h,s=null!=i?i(s,e.value||"",r):s,ue(o)?o.setProperty(u,r,s):Ye(r)||(u.setProperty?u.setProperty(r,s):u[r]=s))}(Ee(),function(){const t=be.lFrame;return fe(t.tView,t.selectedIndex)}(),r,t,e,r[11],n),$s}function zs(t,e,n,r,s){const o=s?"class":"style";fs(t,n,e.inputs[o],o,r)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Gs(t,e,n,r){const s=Ce(),o=Ee(),i=20+t,l=s[11],u=s[i]=rr(l,e,be.lFrame.currentNamespace),a=o.firstCreatePass?function(t,e,n,r,s,o,i){const l=e.consts,u=Hr(e,t,2,s,ye(l,o));return function(t,e,n,r){let s=!1;if(we()){const o=function(t,e,n){const r=t.directiveRegistry;let s=null;if(r)for(let o=0;o<r.length;o++){const i=r[o];Er(n,i.selectors,!1)&&(s||(s=[]),hn(ln(n,e),t,i.type),Xt(i)?(Qr(t,n),s.unshift(i)):s.push(i))}return s}(t,e,n),i=null===r?null:{"":-1};if(null!==o){s=!0,Jr(n,t.data.length,o.length);for(let t=0;t<o.length;t++){const e=o[t];e.providersResolver&&e.providersResolver(e)}let r=!1,l=!1,u=Rr(t,e,o.length,null);for(let s=0;s<o.length;s++){const a=o[s];n.mergedAttrs=Xe(n.mergedAttrs,a.hostAttrs),Kr(t,n,e,u,a),qr(u,a,i),null!==a.contentQueries&&(n.flags|=8),null===a.hostBindings&&null===a.hostAttrs&&0===a.hostVars||(n.flags|=128);const c=a.type.prototype;!r&&(c.ngOnChanges||c.ngOnInit||c.ngDoCheck)&&((t.preOrderHooks||(t.preOrderHooks=[])).push(n.index),r=!0),l||!c.ngOnChanges&&!c.ngDoCheck||((t.preOrderCheckHooks||(t.preOrderCheckHooks=[])).push(n.index),l=!0),u++}!function(t,e){const n=e.directiveEnd,r=t.data,s=e.attrs,o=[];let i=null,l=null;for(let u=e.directiveStart;u<n;u++){const t=r[u],n=t.inputs,a=null===s||mr(e)?null:ts(n,s);o.push(a),i=zr(n,u,i),l=zr(t.outputs,u,l)}null!==i&&(i.hasOwnProperty("class")&&(e.flags|=16),i.hasOwnProperty("style")&&(e.flags|=32)),e.initialInputs=o,e.inputs=i,e.outputs=l}(t,n)}i&&function(t,e,n){if(e){const r=t.localNames=[];for(let t=0;t<e.length;t+=2){const s=n[e[t+1]];if(null==s)throw new rt("301",`Export of name '${e[t+1]}' not found!`);r.push(e[t],s)}}}(n,r,i)}n.mergedAttrs=Xe(n.mergedAttrs,n.attrs)}(e,n,u,ye(l,i)),null!==u.attrs&&ps(u,u.attrs,!1),null!==u.mergedAttrs&&ps(u,u.mergedAttrs,!0),null!==e.queries&&e.queries.elementStart(e,u),u}(i,o,s,0,e,n,r):o.data[i];xe(a,!0);const c=a.mergedAttrs;null!==c&&Ke(l,u,c);const h=a.classes;null!==h&&_r(l,u,h);const d=a.styles;null!==d&&pr(l,u,d),64!=(64&a.flags)&&ar(o,s,u,a),0===be.lFrame.elementDepthCount&&Wn(u,s),be.lFrame.elementDepthCount++,Yt(a)&&(function(t,e,n){we()&&(function(t,e,n,r){const s=n.directiveStart,o=n.directiveEnd;t.firstCreatePass||ln(n,e),Wn(r,e);const i=n.initialInputs;for(let l=s;l<o;l++){const r=t.data[l],o=Xt(r);o&&Yr(e,n,r);const u=mn(e,t,l,n);Wn(u,e),null!==i&&Xr(0,l-s,u,r,0,i),o&&(pe(n.index,e)[8]=u)}}(t,e,n,de(n,e)),128==(128&n.flags)&&function(t,e,n){const r=n.directiveStart,s=n.directiveEnd,o=n.index,i=be.lFrame.currentDirectiveIndex;try{Ze(o);for(let n=r;n<s;n++){const r=t.data[n],s=e[n];De(n),null===r.hostBindings&&0===r.hostVars&&null===r.hostAttrs||Wr(r,s)}}finally{Ze(-1),De(i)}}(t,e,n))}(o,s,a),function(t,e,n){if(Jt(e)){const r=e.directiveEnd;for(let s=e.directiveStart;s<r;s++){const e=t.data[s];e.contentQueries&&e.contentQueries(1,n[s],s)}}}(o,a,s)),null!==r&&function(t,e,n=de){const r=e.localNames;if(null!==r){let s=e.index+1;for(let o=0;o<r.length;o+=2){const i=r[o+1],l=-1===i?n(e,t):t[i];t[s++]=l}}}(s,a)}function Ws(){let t=Ae();Se()?be.lFrame.isParent=!1:(t=t.parent,xe(t,!1));const e=t;be.lFrame.elementDepthCount--;const n=Ee();n.firstCreatePass&&($e(n,t),Jt(t)&&n.queries.elementEnd(t)),null!=e.classesWithoutHost&&function(t){return 0!=(16&t.flags)}(e)&&zs(n,e,Ce(),e.classesWithoutHost,!0),null!=e.stylesWithoutHost&&function(t){return 0!=(32&t.flags)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */(e)&&zs(n,e,Ce(),e.stylesWithoutHost,!1)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Qs(t){return!!t&&"function"==typeof t.then}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function qs(t,e,n=!1,r){const s=Ce(),o=Ee(),i=Ae();return function(t,e,n,r,s,o,i=!1,l){const u=Yt(r),a=t.firstCreatePass&&hs(t),c=cs(e);let h=!0;if(3&r.type){const d=de(r,e),f=l?l(d):Tt,p=f.target||d,_=c.length,g=l?t=>l(ce(t[r.index])).target:r.index;if(ue(n)){let i=null;if(!l&&u&&(i=function(t,e,n,r){const s=t.cleanup;if(null!=s)for(let o=0;o<s.length-1;o+=2){const t=s[o];if(t===n&&s[o+1]===r){const t=e[7],n=s[o+2];return t.length>n?t[n]:null}"string"==typeof t&&(o+=2)}return null}(t,e,s,r.index)),null!==i)(i.__ngLastListenerFn__||i).__ngNextListenerFn__=o,i.__ngLastListenerFn__=o,h=!1;else{o=Ks(r,e,0,o,!1);const t=n.listen(f.name||p,s,o);c.push(o,t),a&&a.push(s,g,_,_+1)}}else o=Ks(r,e,0,o,!0),p.addEventListener(s,o,i),c.push(o),a&&a.push(s,g,_,i)}else o=Ks(r,e,0,o,!1);const d=r.outputs;let f;if(h&&null!==d&&(f=d[s])){const t=f.length;if(t)for(let n=0;n<t;n+=2){const t=e[f[n]][f[n+1]].subscribe(o),i=c.length;c.push(o,t),a&&a.push(s,r.index,i,-(i+1))}}}(o,s,s[11],i,t,e,n,r),qs}function Js(t,e,n,r){try{return!1!==n(r)}catch(s){return ds(t,s),!1}}function Ks(t,e,n,r,s){return function n(o){if(o===Function)return r;const i=2&t.flags?pe(t.index,e):e;0==(32&e[2])&&os(i);let l=Js(e,0,r,o),u=n.__ngNextListenerFn__;for(;u;)l=Js(e,0,u,o)&&l,u=u.__ngNextListenerFn__;return s&&!1===l&&(o.preventDefault(),o.returnValue=!1),l
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}}function Ys(t,e,n,r,s){const o=t[n+1],i=null===e;let l=r?Vr(o):Dr(o),u=!1;for(;0!==l&&(!1===u||i);){const n=t[l+1];Xs(t[l],e)&&(u=!0,t[l+1]=r?jr(n):Or(n)),l=r?Vr(n):Dr(n)}u&&(t[n+1]=r?Or(o):jr(o))}function Xs(t,e){return null===t||null==e||(Array.isArray(t)?t[1]:t)===e||!(!Array.isArray(t)||"string"!=typeof e)&&Vn(t,e)>=0}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function to(t,e){return function(t,e,n,r){const s=Ce(),o=Ee(),i=function(t){const e=be.lFrame,n=e.bindingIndex;return e.bindingIndex=e.bindingIndex+2,n}();o.firstUpdatePass&&function(t,e,n,r){const s=t.data;if(null===s[n+1]){const r=s[Ue()],o=function(t,e){return e>=t.expandoStartIndex}(t,n);(function(t,e){return 0!=(16&t.flags)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */)(r)&&null===e&&!o&&(e=!1),e=function(t,e,n,r){const s=function(t){const e=be.lFrame.currentDirectiveIndex;return-1===e?null:t[e]}(t);let o=e.residualClasses;if(null===s)0===e.classBindings&&(n=no(n=eo(null,t,e,n,true),e.attrs,true),o=null);else{const r=e.directiveStylingLast;if(-1===r||t[r]!==s)if(n=eo(s,t,e,n,true),null===o){let n=function(t,e,n){const r=e.classBindings;if(0!==Dr(r))return t[Vr(r)]}(t,e);void 0!==n&&Array.isArray(n)&&(n=eo(null,t,e,n[1],true),n=no(n,e.attrs,true),function(t,e,n,r){t[Vr(e.classBindings)]=r}(t,e,0,n))}else o=function(t,e,n){let r;const s=e.directiveEnd;for(let o=1+e.directiveStylingLast;o<s;o++)r=no(r,t[o].hostAttrs,true);return no(r,e.attrs,true)}(t,e)}return void 0!==o&&(e.residualClasses=o),n}(s,r,e),function(t,e,n,r,s,o){let i=e.classBindings,l=Vr(i),u=Dr(i);t[r]=n;let a,c=!1;if(Array.isArray(n)){const t=n;a=t[1],(null===a||Vn(t,a)>0)&&(c=!0)}else a=n;if(s)if(0!==u){const e=Vr(t[l+1]);t[r+1]=Ir(e,l),0!==e&&(t[e+1]=Pr(t[e+1],r)),t[l+1]=131071&t[l+1]|r<<17}else t[r+1]=Ir(l,0),0!==l&&(t[l+1]=Pr(t[l+1],r)),l=r;else t[r+1]=Ir(u,0),0===l?l=r:t[u+1]=Pr(t[u+1],r),u=r;c&&(t[r+1]=Or(t[r+1])),Ys(t,a,r,!0),Ys(t,a,r,!1),function(t,e,n,r,s){const o=t.residualClasses;null!=o&&"string"==typeof e&&Vn(o,e)>=0&&(n[r+1]=jr(n[r+1]))}(e,a,t,r),i=Ir(l,u),e.classBindings=i}(s,r,e,n,o)}}(o,t,i),e!==xr&&Us(s,i,e)&&function(t,e,n,r,s,o,i,l){if(!(3&e.type))return;const u=t.data,a=u[l+1];so(1==(1&a)?ro(u,e,n,s,Dr(a),true):void 0)||(so(o)||function(t){return 2==(2&t)}(a)&&(o=ro(u,null,n,s,l,true)),function(t,e,n,r,s){const o=ue(t);s?o?t.addClass(n,r):n.classList.add(r):o?t.removeClass(n,r):n.classList.remove(r)}(r,0,he(Ue(),n),s,o))}(o,o.data[Ue()],s,s[11],t,s[i+1]=function(t,e){return null==t||"object"==typeof t&&(t=K(function(t){return t instanceof
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
class{constructor(t){this.changingThisBreaksApplicationSecurity=t}toString(){return`SafeValue must use [property]=binding: ${this.changingThisBreaksApplicationSecurity} (see https://g.co/ng/security#xss)`}}?t.changingThisBreaksApplicationSecurity:t}(t))),t}(e),0,i)}(t,e),to}function eo(t,e,n,r,s){let o=null;const i=n.directiveEnd;let l=n.directiveStylingLast;for(-1===l?l=n.directiveStart:l++;l<i&&(o=e[l],r=no(r,o.hostAttrs,s),o!==t);)l++;return null!==t&&(n.directiveStylingLast=l),r}function no(t,e,n){const r=n?1:2;let s=-1;if(null!==e)for(let o=0;o<e.length;o++){const i=e[o];"number"==typeof i?s=i:s===r&&(Array.isArray(t)||(t=void 0===t?[]:["",t]),Tn(t,i,!!n||e[++o]))}return void 0===t?null:t}function ro(t,e,n,r,s,o){const i=null===e;let l;for(;s>0;){const e=t[s],o=Array.isArray(e),u=o?e[1]:e,a=null===u;let c=n[s+1];c===xr&&(c=a?It:void 0);let h=a?In(c,r):u===r?c:void 0;if(o&&!so(h)&&(h=In(e,r)),so(h)&&(l=h,i))return l;const d=t[s+1];s=i?Vr(d):Dr(d)}if(null!==e){let t=o?e.residualClasses:e.residualStyles;null!=t&&(l=In(t,r))}return l}function so(t){return void 0!==t}function oo(t,e=""){const n=Ce(),r=Ee(),s=t+20,o=r.firstCreatePass?Hr(r,s,1,e,null):r.data[s],i=n[s]=function(t,e){return ue(t)?t.createText(e):t.createTextNode(e)}(n[11],e);ar(r,n,i,o),xe(o,!1)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function io(t,e,n){const r=Ce(),s=function(t,e,n,r){return Us(t,Ve(),n)?e+st(n)+r:xr}(r,t,e,n);return s!==xr&&function(t,e,n){const r=he(e,t);!function(t,e,n){ue(t)?t.setValue(e,n):e.textContent=n}(t[11],r,n)}(r,Ue(),s),io}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const lo=void 0;var uo=["en",[["a","p"],["AM","PM"],lo],[["AM","PM"],lo,lo],[["S","M","T","W","T","F","S"],["Sun","Mon","Tue","Wed","Thu","Fri","Sat"],["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"],["Su","Mo","Tu","We","Th","Fr","Sa"]],lo,[["J","F","M","A","M","J","J","A","S","O","N","D"],["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"],["January","February","March","April","May","June","July","August","September","October","November","December"]],lo,[["B","A"],["BC","AD"],["Before Christ","Anno Domini"]],0,[6,0],["M/d/yy","MMM d, y","MMMM d, y","EEEE, MMMM d, y"],["h:mm a","h:mm:ss a","h:mm:ss a z","h:mm:ss a zzzz"],["{1}, {0}",lo,"{1} 'at' {0}",lo],[".",",",";","%","+","-","E","\xd7","\u2030","\u221e","NaN",":"],["#,##0.###","#,##0%","\xa4#,##0.00","#E0"],"USD","$","US Dollar",{},"ltr",function(t){let e=Math.floor(Math.abs(t)),n=t.toString().replace(/^[^.]*\.?/,"").length;return 1===e&&0===n?1:5}];
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let ao={};function co(t){return t in ao||(ao[t]=St.ng&&St.ng.common&&St.ng.common.locales&&St.ng.common.locales[t]),ao[t]}var ho=function(t){return t[t.LocaleId=0]="LocaleId",t[t.DayPeriodsFormat=1]="DayPeriodsFormat",t[t.DayPeriodsStandalone=2]="DayPeriodsStandalone",t[t.DaysFormat=3]="DaysFormat",t[t.DaysStandalone=4]="DaysStandalone",t[t.MonthsFormat=5]="MonthsFormat",t[t.MonthsStandalone=6]="MonthsStandalone",t[t.Eras=7]="Eras",t[t.FirstDayOfWeek=8]="FirstDayOfWeek",t[t.WeekendRange=9]="WeekendRange",t[t.DateFormat=10]="DateFormat",t[t.TimeFormat=11]="TimeFormat",t[t.DateTimeFormat=12]="DateTimeFormat",t[t.NumberSymbols=13]="NumberSymbols",t[t.NumberFormats=14]="NumberFormats",t[t.CurrencyCode=15]="CurrencyCode",t[t.CurrencySymbol=16]="CurrencySymbol",t[t.CurrencyName=17]="CurrencyName",t[t.Currencies=18]="Currencies",t[t.Directionality=19]="Directionality",t[t.PluralCase=20]="PluralCase",t[t.ExtraData=21]="ExtraData",t}({});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let fo="en-US";function po(t){var e,n;n="Expected localeId to be defined",null==(e=t)&&function(t,e,n,r){throw new Error(`ASSERTION ERROR: ${t} [Expected=> null != ${e} <=Actual]`)}(n,e),"string"==typeof t&&(fo=t.toLowerCase().replace(/_/g,"-"))}function _o(t,e,n,r,s){if(t=et(t),Array.isArray(t))for(let o=0;o<t.length;o++)_o(t[o],e,n,r,s);else{const o=Ee(),i=Ce();let l=Is(t)?t:et(t.provide),u=xs(t);const a=Ae(),c=1048575&a.providerIndexes,h=a.directiveStart,d=a.providerIndexes>>20;if(Is(t)||!t.multi){const r=new Je(u,s,Zs),f=mo(l,e,s?c:c+d,h);-1===f?(hn(ln(a,i),o,l),go(o,t,e.length),e.push(l),a.directiveStart++,a.directiveEnd++,s&&(a.providerIndexes+=1048576),n.push(r),i.push(r)):(n[f]=r,i[f]=r)}else{const f=mo(l,e,c+d,h),p=mo(l,e,c,c+d),_=f>=0&&n[f],g=p>=0&&n[p];if(s&&!g||!s&&!_){hn(ln(a,i),o,l);const c=function(t,e,n,r,s){const o=new Je(t,n,Zs);return o.multi=[],o.index=e,o.componentProviders=0,yo(o,s,r&&!n),o}(s?bo:vo,n.length,s,r,u);!s&&g&&(n[p].providerFactory=c),go(o,t,e.length,0),e.push(l),a.directiveStart++,a.directiveEnd++,s&&(a.providerIndexes+=1048576),n.push(c),i.push(c)}else go(o,t,f>-1?f:p,yo(n[s?p:f],u,!s&&r));!s&&r&&g&&n[p].componentProviders++}}}function go(t,e,n,r){const s=Is(e);if(s||e.useClass){const o=(e.useClass||e).prototype.ngOnDestroy;if(o){const i=t.destroyHooks||(t.destroyHooks=[]);if(!s&&e.multi){const t=i.indexOf(n);-1===t?i.push(n,[r,o]):i[t+1].push(r,o)}else i.push(n,o)}}}function yo(t,e,n){return n&&t.componentProviders++,t.multi.push(e)-1}function mo(t,e,n,r){for(let s=n;s<r;s++)if(e[s]===t)return s;return-1}function vo(t,e,n,r){return wo(this.multi,[])}function bo(t,e,n,r){const s=this.multi;let o;if(this.providerFactory){const t=this.providerFactory.componentProviders,e=mn(n,n[1],this.providerFactory.index,r);o=e.slice(0,t),wo(s,o);for(let n=t;n<e.length;n++)o.push(e[n])}else o=[],wo(s,o);return o}function wo(t,e){for(let n=0;n<t.length;n++)e.push((0,t[n])());return e}function Co(t,e=[]){return n=>{n.providersResolver=(n,r)=>
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
function(t,e,n){const r=Ee();if(r.firstCreatePass){const s=Xt(t);_o(n,r.data,r.blueprint,s,!0),_o(e,r.data,r.blueprint,s,!1)}}(n,r?r(t):t,e)}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Eo{}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Ao{resolveComponentFactory(t){throw function(t){const e=Error(`No component factory found for ${K(t)}. Did you add it to @NgModule.entryComponents?`);return e.ngComponent=t,e}(t)}}let ko=(()=>{class t{}return t.NULL=new Ao,t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function xo(...t){}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function So(t,e){return new Io(de(t,e))}const To=function(){return So(Ae(),Ce())};let Io=(()=>{class t{constructor(t){this.nativeElement=t}}return t.__NG_ELEMENT_ID__=To,t})();class Vo{}let Oo=(()=>{class t{}return t.__NG_ELEMENT_ID__=()=>Do(),t})();const Do=function(){const t=Ce(),e=pe(Ae().index,t);return function(t){return t[11]}(Qt(e)?e:t)};let Po=(()=>{class t{}return t.\u0275prov=lt({token:t,providedIn:"root",factory:()=>null}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class jo{constructor(t){this.full=t,this.major=t.split(".")[0],this.minor=t.split(".")[1],this.patch=t.split(".").slice(2).join(".")}}const Mo=new jo("11.2.11");
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class No{constructor(){}supports(t){return Ls(t)}create(t){return new Ro(t)}}const Ho=(t,e)=>e;class Ro{constructor(t){this.length=0,this._linkedRecords=null,this._unlinkedRecords=null,this._previousItHead=null,this._itHead=null,this._itTail=null,this._additionsHead=null,this._additionsTail=null,this._movesHead=null,this._movesTail=null,this._removalsHead=null,this._removalsTail=null,this._identityChangesHead=null,this._identityChangesTail=null,this._trackByFn=t||Ho}forEachItem(t){let e;for(e=this._itHead;null!==e;e=e._next)t(e)}forEachOperation(t){let e=this._itHead,n=this._removalsHead,r=0,s=null;for(;e||n;){const o=!n||e&&e.currentIndex<Uo(n,r,s)?e:n,i=Uo(o,r,s),l=o.currentIndex;if(o===n)r--,n=n._nextRemoved;else if(e=e._next,null==o.previousIndex)r++;else{s||(s=[]);const t=i-r,e=l-r;if(t!=e){for(let n=0;n<t;n++){const r=n<s.length?s[n]:s[n]=0,o=r+n;e<=o&&o<t&&(s[n]=r+1)}s[o.previousIndex]=e-t}}i!==l&&t(o,i,l)}}forEachPreviousItem(t){let e;for(e=this._previousItHead;null!==e;e=e._nextPrevious)t(e)}forEachAddedItem(t){let e;for(e=this._additionsHead;null!==e;e=e._nextAdded)t(e)}forEachMovedItem(t){let e;for(e=this._movesHead;null!==e;e=e._nextMoved)t(e)}forEachRemovedItem(t){let e;for(e=this._removalsHead;null!==e;e=e._nextRemoved)t(e)}forEachIdentityChange(t){let e;for(e=this._identityChangesHead;null!==e;e=e._nextIdentityChange)t(e)}diff(t){if(null==t&&(t=[]),!Ls(t))throw new Error(`Error trying to diff '${K(t)}'. Only arrays and iterables are allowed`);return this.check(t)?this:null}onDestroy(){}check(t){this._reset();let e,n,r,s=this._itHead,o=!1;if(Array.isArray(t)){this.length=t.length;for(let e=0;e<this.length;e++)n=t[e],r=this._trackByFn(e,n),null!==s&&Object.is(s.trackById,r)?(o&&(s=this._verifyReinsertion(s,n,r,e)),Object.is(s.item,n)||this._addIdentityChange(s,n)):(s=this._mismatch(s,n,r,e),o=!0),s=s._next}else e=0,function(t,e){if(Array.isArray(t))for(let n=0;n<t.length;n++)e(t[n]);else{const n=t[Fs()]();let r;for(;!(r=n.next()).done;)e(r.value)}}(t,t=>{r=this._trackByFn(e,t),null!==s&&Object.is(s.trackById,r)?(o&&(s=this._verifyReinsertion(s,t,r,e)),Object.is(s.item,t)||this._addIdentityChange(s,t)):(s=this._mismatch(s,t,r,e),o=!0),s=s._next,e++}),this.length=e;return this._truncate(s),this.collection=t,this.isDirty}get isDirty(){return null!==this._additionsHead||null!==this._movesHead||null!==this._removalsHead||null!==this._identityChangesHead}_reset(){if(this.isDirty){let t;for(t=this._previousItHead=this._itHead;null!==t;t=t._next)t._nextPrevious=t._next;for(t=this._additionsHead;null!==t;t=t._nextAdded)t.previousIndex=t.currentIndex;for(this._additionsHead=this._additionsTail=null,t=this._movesHead;null!==t;t=t._nextMoved)t.previousIndex=t.currentIndex;this._movesHead=this._movesTail=null,this._removalsHead=this._removalsTail=null,this._identityChangesHead=this._identityChangesTail=null}}_mismatch(t,e,n,r){let s;return null===t?s=this._itTail:(s=t._prev,this._remove(t)),null!==(t=null===this._unlinkedRecords?null:this._unlinkedRecords.get(n,null))?(Object.is(t.item,e)||this._addIdentityChange(t,e),this._reinsertAfter(t,s,r)):null!==(t=null===this._linkedRecords?null:this._linkedRecords.get(n,r))?(Object.is(t.item,e)||this._addIdentityChange(t,e),this._moveAfter(t,s,r)):t=this._addAfter(new Fo(e,n),s,r),t}_verifyReinsertion(t,e,n,r){let s=null===this._unlinkedRecords?null:this._unlinkedRecords.get(n,null);return null!==s?t=this._reinsertAfter(s,t._prev,r):t.currentIndex!=r&&(t.currentIndex=r,this._addToMoves(t,r)),t}_truncate(t){for(;null!==t;){const e=t._next;this._addToRemovals(this._unlink(t)),t=e}null!==this._unlinkedRecords&&this._unlinkedRecords.clear(),null!==this._additionsTail&&(this._additionsTail._nextAdded=null),null!==this._movesTail&&(this._movesTail._nextMoved=null),null!==this._itTail&&(this._itTail._next=null),null!==this._removalsTail&&(this._removalsTail._nextRemoved=null),null!==this._identityChangesTail&&(this._identityChangesTail._nextIdentityChange=null)}_reinsertAfter(t,e,n){null!==this._unlinkedRecords&&this._unlinkedRecords.remove(t);const r=t._prevRemoved,s=t._nextRemoved;return null===r?this._removalsHead=s:r._nextRemoved=s,null===s?this._removalsTail=r:s._prevRemoved=r,this._insertAfter(t,e,n),this._addToMoves(t,n),t}_moveAfter(t,e,n){return this._unlink(t),this._insertAfter(t,e,n),this._addToMoves(t,n),t}_addAfter(t,e,n){return this._insertAfter(t,e,n),this._additionsTail=null===this._additionsTail?this._additionsHead=t:this._additionsTail._nextAdded=t,t}_insertAfter(t,e,n){const r=null===e?this._itHead:e._next;return t._next=r,t._prev=e,null===r?this._itTail=t:r._prev=t,null===e?this._itHead=t:e._next=t,null===this._linkedRecords&&(this._linkedRecords=new Bo),this._linkedRecords.put(t),t.currentIndex=n,t}_remove(t){return this._addToRemovals(this._unlink(t))}_unlink(t){null!==this._linkedRecords&&this._linkedRecords.remove(t);const e=t._prev,n=t._next;return null===e?this._itHead=n:e._next=n,null===n?this._itTail=e:n._prev=e,t}_addToMoves(t,e){return t.previousIndex===e||(this._movesTail=null===this._movesTail?this._movesHead=t:this._movesTail._nextMoved=t),t}_addToRemovals(t){return null===this._unlinkedRecords&&(this._unlinkedRecords=new Bo),this._unlinkedRecords.put(t),t.currentIndex=null,t._nextRemoved=null,null===this._removalsTail?(this._removalsTail=this._removalsHead=t,t._prevRemoved=null):(t._prevRemoved=this._removalsTail,this._removalsTail=this._removalsTail._nextRemoved=t),t}_addIdentityChange(t,e){return t.item=e,this._identityChangesTail=null===this._identityChangesTail?this._identityChangesHead=t:this._identityChangesTail._nextIdentityChange=t,t}}class Fo{constructor(t,e){this.item=t,this.trackById=e,this.currentIndex=null,this.previousIndex=null,this._nextPrevious=null,this._prev=null,this._next=null,this._prevDup=null,this._nextDup=null,this._prevRemoved=null,this._nextRemoved=null,this._nextAdded=null,this._nextMoved=null,this._nextIdentityChange=null}}class Lo{constructor(){this._head=null,this._tail=null}add(t){null===this._head?(this._head=this._tail=t,t._nextDup=null,t._prevDup=null):(this._tail._nextDup=t,t._prevDup=this._tail,t._nextDup=null,this._tail=t)}get(t,e){let n;for(n=this._head;null!==n;n=n._nextDup)if((null===e||e<=n.currentIndex)&&Object.is(n.trackById,t))return n;return null}remove(t){const e=t._prevDup,n=t._nextDup;return null===e?this._head=n:e._nextDup=n,null===n?this._tail=e:n._prevDup=e,null===this._head}}class Bo{constructor(){this.map=new Map}put(t){const e=t.trackById;let n=this.map.get(e);n||(n=new Lo,this.map.set(e,n)),n.add(t)}get(t,e){const n=this.map.get(t);return n?n.get(t,e):null}remove(t){const e=t.trackById;return this.map.get(e).remove(t)&&this.map.delete(e),t}get isEmpty(){return 0===this.map.size}clear(){this.map.clear()}}function Uo(t,e,n){const r=t.previousIndex;if(null===r)return r;let s=0;return n&&r<n.length&&(s=n[r]),r+e+s}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Zo{constructor(){}supports(t){return t instanceof Map||Bs(t)}create(){return new $o}}class $o{constructor(){this._records=new Map,this._mapHead=null,this._appendAfter=null,this._previousMapHead=null,this._changesHead=null,this._changesTail=null,this._additionsHead=null,this._additionsTail=null,this._removalsHead=null,this._removalsTail=null}get isDirty(){return null!==this._additionsHead||null!==this._changesHead||null!==this._removalsHead}forEachItem(t){let e;for(e=this._mapHead;null!==e;e=e._next)t(e)}forEachPreviousItem(t){let e;for(e=this._previousMapHead;null!==e;e=e._nextPrevious)t(e)}forEachChangedItem(t){let e;for(e=this._changesHead;null!==e;e=e._nextChanged)t(e)}forEachAddedItem(t){let e;for(e=this._additionsHead;null!==e;e=e._nextAdded)t(e)}forEachRemovedItem(t){let e;for(e=this._removalsHead;null!==e;e=e._nextRemoved)t(e)}diff(t){if(t){if(!(t instanceof Map||Bs(t)))throw new Error(`Error trying to diff '${K(t)}'. Only maps and objects are allowed`)}else t=new Map;return this.check(t)?this:null}onDestroy(){}check(t){this._reset();let e=this._mapHead;if(this._appendAfter=null,this._forEach(t,(t,n)=>{if(e&&e.key===n)this._maybeAddToChanges(e,t),this._appendAfter=e,e=e._next;else{const r=this._getOrCreateRecordForKey(n,t);e=this._insertBeforeOrAppend(e,r)}}),e){e._prev&&(e._prev._next=null),this._removalsHead=e;for(let t=e;null!==t;t=t._nextRemoved)t===this._mapHead&&(this._mapHead=null),this._records.delete(t.key),t._nextRemoved=t._next,t.previousValue=t.currentValue,t.currentValue=null,t._prev=null,t._next=null}return this._changesTail&&(this._changesTail._nextChanged=null),this._additionsTail&&(this._additionsTail._nextAdded=null),this.isDirty}_insertBeforeOrAppend(t,e){if(t){const n=t._prev;return e._next=t,e._prev=n,t._prev=e,n&&(n._next=e),t===this._mapHead&&(this._mapHead=e),this._appendAfter=t,t}return this._appendAfter?(this._appendAfter._next=e,e._prev=this._appendAfter):this._mapHead=e,this._appendAfter=e,null}_getOrCreateRecordForKey(t,e){if(this._records.has(t)){const n=this._records.get(t);this._maybeAddToChanges(n,e);const r=n._prev,s=n._next;return r&&(r._next=s),s&&(s._prev=r),n._next=null,n._prev=null,n}const n=new zo(t);return this._records.set(t,n),n.currentValue=e,this._addToAdditions(n),n}_reset(){if(this.isDirty){let t;for(this._previousMapHead=this._mapHead,t=this._previousMapHead;null!==t;t=t._next)t._nextPrevious=t._next;for(t=this._changesHead;null!==t;t=t._nextChanged)t.previousValue=t.currentValue;for(t=this._additionsHead;null!=t;t=t._nextAdded)t.previousValue=t.currentValue;this._changesHead=this._changesTail=null,this._additionsHead=this._additionsTail=null,this._removalsHead=null}}_maybeAddToChanges(t,e){Object.is(e,t.currentValue)||(t.previousValue=t.currentValue,t.currentValue=e,this._addToChanges(t))}_addToAdditions(t){null===this._additionsHead?this._additionsHead=this._additionsTail=t:(this._additionsTail._nextAdded=t,this._additionsTail=t)}_addToChanges(t){null===this._changesHead?this._changesHead=this._changesTail=t:(this._changesTail._nextChanged=t,this._changesTail=t)}_forEach(t,e){t instanceof Map?t.forEach(e):Object.keys(t).forEach(n=>e(t[n],n))}}class zo{constructor(t){this.key=t,this.previousValue=null,this.currentValue=null,this._nextPrevious=null,this._next=null,this._prev=null,this._nextAdded=null,this._nextRemoved=null,this._nextChanged=null
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}}function Go(){return new Wo([new No])}let Wo=(()=>{class t{constructor(t){this.factories=t}static create(e,n){if(null!=n){const t=n.factories.slice();e=e.concat(t)}return new t(e)}static extend(e){return{provide:t,useFactory:n=>t.create(e,n||Go()),deps:[[t,new Un,new Bn]]}}find(t){const e=this.factories.find(e=>e.supports(t));if(null!=e)return e;throw new Error(`Cannot find a differ supporting object '${t}' of type '${n=t,n.name||typeof n}'`);var n;
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}}return t.\u0275prov=lt({token:t,providedIn:"root",factory:Go}),t})();function Qo(){return new qo([new Zo])}let qo=(()=>{class t{constructor(t){this.factories=t}static create(e,n){if(n){const t=n.factories.slice();e=e.concat(t)}return new t(e)}static extend(e){return{provide:t,useFactory:n=>t.create(e,n||Qo()),deps:[[t,new Un,new Bn]]}}find(t){const e=this.factories.find(e=>e.supports(t));if(e)return e;throw new Error(`Cannot find a differ supporting object '${t}'`)}}return t.\u0275prov=lt({token:t,providedIn:"root",factory:Qo}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function Jo(t,e,n,r,s=!1){for(;null!==n;){const o=e[n.index];if(null!==o&&r.push(ce(o)),qt(o))for(let t=10;t<o.length;t++){const e=o[t],n=e[1].firstChild;null!==n&&Jo(e[1],e,n,r)}const i=n.type;if(8&i)Jo(t,e,n.child,r);else if(32&i){const t=Kn(n,e);let s;for(;s=t();)r.push(s)}else if(16&i){const t=cr(e,n);if(Array.isArray(t))r.push(...t);else{const n=Yn(e[16]);Jo(n[1],n,t,r,!0)}}n=s?n.projectionNext:n.next}return r}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Ko extends class{constructor(t,e){this._lView=t,this._cdRefInjectingView=e,this._appRef=null,this._attachedToViewContainer=!1}get rootNodes(){const t=this._lView,e=t[1];return Jo(e,t,e.firstChild,[])}get context(){return this._lView[8]}get destroyed(){return 256==(256&this._lView[2])}destroy(){if(this._appRef)this._appRef.detachView(this);else if(this._attachedToViewContainer){const t=this._lView[3];if(qt(t)){const e=t[8],n=e?e.indexOf(this):-1;n>-1&&(function(t,e){if(t.length<=10)return;const n=10+e,r=t[n];if(r){const o=r[17];null!==o&&o!==t&&sr(o,r),e>0&&(t[n-1][4]=r[4]);const i=Sn(t,10+e);dr(r[1],s=r,s[11],2,null,null),s[0]=null,s[6]=null;const l=i[19];null!==l&&l.detachView(i[1]),r[3]=null,r[4]=null,r[2]&=-129}var s}(t,n),Sn(e,n))}this._attachedToViewContainer=!1}!function(t,e){if(!(256&e[2])){const n=e[11];ue(n)&&n.destroyNode&&dr(t,e,n,3,null,null),function(t){let e=t[13];if(!e)return or(t[1],t);for(;e;){let n=null;if(Qt(e))n=e[13];else{const t=e[10];t&&(n=t)}if(!n){for(;e&&!e[4]&&e!==t;)Qt(e)&&or(e[1],e),e=e[3];null===e&&(e=t),Qt(e)&&or(e[1],e),n=e&&e[4]}e=n}}(e)}}(this._lView[1],this._lView)}onDestroy(t){!function(t,e,n,r){const s=cs(e);s.push(r)}(0,this._lView,0,t)}markForCheck(){os(this._cdRefInjectingView||this._lView)}detach(){this._lView[2]&=-129}reattach(){this._lView[2]|=128}detectChanges(){is(this._lView[1],this._lView,this.context)}checkNoChanges(){!function(t,e,n){Ie(!0);try{is(t,e,n)}finally{Ie(!1)}}(this._lView[1],this._lView,this.context)}attachToViewContainerRef(){if(this._appRef)throw new Error("This view is already attached directly to the ApplicationRef!");this._attachedToViewContainer=!0}detachFromAppRef(){var t;this._appRef=null,dr(this._lView[1],t=this._lView,t[11],2,null,null)}attachToAppRef(t){if(this._attachedToViewContainer)throw new Error("This view is already attached to a ViewContainer!");this._appRef=t}}{constructor(t){super(t),this._view=t}detectChanges(){ls(this._view)}checkNoChanges(){!function(t){Ie(!0);try{ls(t)}finally{Ie(!1)}}(this._view)}get context(){return null}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Yo=[new Zo],Xo=new Wo([new No]),ti=new qo(Yo);
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ei{}const ni={};
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ri extends ko{constructor(t){super(),this.ngModule=t}resolveComponentFactory(t){const e=Gt(t);return new ii(e,this.ngModule)}}function si(t){const e=[];for(let n in t)t.hasOwnProperty(n)&&e.push({propName:t[n],templateName:n});return e}const oi=new kn("SCHEDULER_TOKEN",{providedIn:"root",factory:()=>Qn});class ii extends Eo{constructor(t,e){super(),this.componentDef=t,this.ngModule=e,this.componentType=t.type,this.selector=t.selectors.map(kr).join(","),this.ngContentSelectors=t.ngContentSelectors?t.ngContentSelectors:[],this.isBoundToModule=!!e}get inputs(){return si(this.componentDef.inputs)}get outputs(){return si(this.componentDef.outputs)}create(t,e,n,r){const s=(r=r||this.ngModule)?function(t,e){return{get:(n,r,s)=>{const o=t.get(n,ni,s);return o!==ni||r===ni?o:e.get(n,r,s)}}}(t,r.injector):t,o=s.get(Vo,ae),i=s.get(Po,null),l=o.createRenderer(null,this.componentDef),u=this.componentDef.selectors[0][0]||"div",a=n?function(t,e,n){if(ue(t))return t.selectRootElement(e,n===Ct.ShadowDom);let r="string"==typeof e?t.querySelector(e):e;return r.textContent="",r}(l,n,this.componentDef.encapsulation):rr(o.createRenderer(null,this.componentDef),u,function(t){const e=t.toLowerCase();return"svg"===e?"http://www.w3.org/2000/svg":"math"===e?"http://www.w3.org/1998/MathML/":null}(u)),c=this.componentDef.onPush?576:528,h={components:[],scheduler:Qn,clean:as,playerHandler:null,flags:0},d=$r(0,null,null,1,0,null,null,null,null,null),f=Nr(null,d,h,c,null,null,o,l,i,s);let p,_;Ne(f);try{const t=function(t,e,n,r,s,o){const i=n[1];n[20]=t;const l=Hr(i,20,2,"#host",null),u=l.mergedAttrs=e.hostAttrs;null!==u&&(ps(l,u,!0),null!==t&&(Ke(s,t,u),null!==l.classes&&_r(s,t,l.classes),null!==l.styles&&pr(s,t,l.styles)));const a=r.createRenderer(t,e),c=Nr(n,Zr(e),null,e.onPush?64:16,n[20],l,r,a,null,null);return i.firstCreatePass&&(hn(ln(l,n),i,e.type),Qr(i,l),Jr(l,n.length,1)),ss(n,c),n[20]=c}(a,this.componentDef,f,o,l);if(a)if(n)Ke(l,a,["ng-version",Mo.full]);else{const{attrs:t,classes:e}=function(t){const e=[],n=[];let r=1,s=2;for(;r<t.length;){let o=t[r];if("string"==typeof o)2===s?""!==o&&e.push(o,t[++r]):8===s&&n.push(o);else{if(!wr(s))break;s=o}r++}return{attrs:e,classes:n}}(this.componentDef.selectors[0]);t&&Ke(l,a,t),e&&e.length>0&&_r(l,a,e.join(" "))}if(_=fe(d,20),void 0!==e){const t=_.projection=[];for(let n=0;n<this.ngContentSelectors.length;n++){const r=e[n];t.push(null!=r?Array.from(r):null)}}p=function(t,e,n,r,s){const o=n[1],i=function(t,e,n){const r=Ae();t.firstCreatePass&&(n.providersResolver&&n.providersResolver(n),Kr(t,r,e,Rr(t,e,1,null),n));const s=mn(e,t,r.directiveStart,r);Wn(s,e);const o=de(r,e);return o&&Wn(o,e),s}(o,n,e);if(r.components.push(i),t[8]=i,s&&s.forEach(t=>t(i,e)),e.contentQueries){const t=Ae();e.contentQueries(1,i,t.directiveStart)}const l=Ae();return!o.firstCreatePass||null===e.hostBindings&&null===e.hostAttrs||(Ze(l.index),Gr(n[1],l,0,l.directiveStart,l.directiveEnd,e),Wr(e,i)),i}(t,this.componentDef,f,h,[Ds]),Fr(d,f,null)}finally{Be()}return new li(this.componentType,p,So(_,f),f,_)}}class li extends class{}{constructor(t,e,n,r,s){super(),this.location=n,this._rootLView=r,this._tNode=s,this.instance=e,this.hostView=this.changeDetectorRef=new Ko(r),this.componentType=t}get injector(){return new wn(this._tNode,this._rootLView)}destroy(){this.hostView.destroy()}onDestroy(t){this.hostView.onDestroy(t)}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const ui=new Map;
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ai extends ei{constructor(t,e){super(),this._parent=e,this._bootstrapComponents=[],this.injector=this,this.destroyCbs=[],this.componentFactoryResolver=new ri(this);const n=Wt(t),r=t[Mt]||null;r&&po(r),this._bootstrapComponents=qn(n.bootstrap),this._r3Injector=Es(t,e,[{provide:ei,useValue:this},{provide:ko,useValue:this.componentFactoryResolver}],K(t)),this._r3Injector._resolveInjectorDefTypes(),this.instance=this.get(t)}get(t,e=Os.THROW_IF_NOT_FOUND,n=gt.Default){return t===Os||t===ei||t===_s?this:this._r3Injector.get(t,e,n)}destroy(){const t=this._r3Injector;!t.destroyed&&t.destroy(),this.destroyCbs.forEach(t=>t()),this.destroyCbs=null}onDestroy(t){this.destroyCbs.push(t)}}class ci extends class{}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */{constructor(t){super(),this.moduleType=t,null!==Wt(t)&&function(t){const e=new Set;!function t(n){const r=Wt(n,!0),s=r.id;null!==s&&(function(t,e,n){if(e&&e!==n)throw new Error(`Duplicate module registered for ${t} - ${K(e)} vs ${K(e.name)}`)}(s,ui.get(s),n),ui.set(s,n));const o=qn(r.imports);for(const i of o)e.has(i)||(e.add(i),t(i))}(t)}(t)}create(t){return new ai(this.moduleType,t)}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */function hi(t){return e=>{setTimeout(t,void 0,e)}}const di=
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
class extends E{constructor(t=!1){super(),this.__isAsync=t}emit(t){super.next(t)}subscribe(t,e,n){var r,s,o;let i=t,l=e||(()=>null),u=n;if(t&&"object"==typeof t){const e=t;i=null===(r=e.next)||void 0===r?void 0:r.bind(e),l=null===(s=e.error)||void 0===s?void 0:s.bind(e),u=null===(o=e.complete)||void 0===o?void 0:o.bind(e)}this.__isAsync&&(l=hi(l),i&&(i=hi(i)),u&&(u=hi(u)));const a=super.subscribe({next:i,error:l,complete:u});return t instanceof h&&t.add(a),a}},fi=new kn("Application Initializer");
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let pi=(()=>{class t{constructor(t){this.appInits=t,this.resolve=xo,this.reject=xo,this.initialized=!1,this.done=!1,this.donePromise=new Promise((t,e)=>{this.resolve=t,this.reject=e})}runInitializers(){if(this.initialized)return;const t=[],e=()=>{this.done=!0,this.resolve()};if(this.appInits)for(let n=0;n<this.appInits.length;n++){const e=this.appInits[n]();Qs(e)&&t.push(e)}Promise.all(t).then(()=>{e()}).catch(t=>{this.reject(t)}),0===t.length&&e(),this.initialized=!0}}return t.\u0275fac=function(e){return new(e||t)(Hn(fi,8))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const _i=new kn("AppId"),gi={provide:_i,useFactory:function(){return`${yi()}${yi()}${yi()}`},deps:[]};function yi(){return String.fromCharCode(97+Math.floor(25*Math.random()))}const mi=new kn("Platform Initializer"),vi=new kn("Platform ID"),bi=new kn("appBootstrapListener");let wi=(()=>{class t{log(t){console.log(t)}warn(t){console.warn(t)}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Ci=new kn("LocaleId"),Ei=new kn("DefaultCurrencyCode");
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class Ai{constructor(t,e){this.ngModuleFactory=t,this.componentFactories=e}}const ki=function(t){return new ci(t)},xi=ki,Si=function(t){return Promise.resolve(ki(t))},Ti=function(t){const e=ki(t),n=qn(Wt(t).declarations).reduce((t,e)=>{const n=Gt(e);return n&&t.push(new ii(n)),t},[]);return new Ai(e,n)},Ii=Ti,Vi=function(t){return Promise.resolve(Ti(t))};let Oi=(()=>{class t{constructor(){this.compileModuleSync=xi,this.compileModuleAsync=Si,this.compileModuleAndAllComponentsSync=Ii,this.compileModuleAndAllComponentsAsync=Vi}clearCache(){}clearCacheFor(t){}getModuleId(t){}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();const Di=(()=>Promise.resolve(0))();function Pi(t){"undefined"==typeof Zone?Di.then(()=>{t&&t.apply(null,null)}):Zone.current.scheduleMicroTask("scheduleMicrotask",t)}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ji{constructor({enableLongStackTrace:t=!1,shouldCoalesceEventChangeDetection:e=!1,shouldCoalesceRunChangeDetection:n=!1}){if(this.hasPendingMacrotasks=!1,this.hasPendingMicrotasks=!1,this.isStable=!0,this.onUnstable=new di(!1),this.onMicrotaskEmpty=new di(!1),this.onStable=new di(!1),this.onError=new di(!1),"undefined"==typeof Zone)throw new Error("In this configuration Angular requires Zone.js");Zone.assertZonePatched(),this._nesting=0,this._outer=this._inner=Zone.current,Zone.TaskTrackingZoneSpec&&(this._inner=this._inner.fork(new Zone.TaskTrackingZoneSpec)),t&&Zone.longStackTraceZoneSpec&&(this._inner=this._inner.fork(Zone.longStackTraceZoneSpec)),this.shouldCoalesceEventChangeDetection=!n&&e,this.shouldCoalesceRunChangeDetection=n,this.lastRequestAnimationFrameId=-1,this.nativeRequestAnimationFrame=function(){let t=St.requestAnimationFrame,e=St.cancelAnimationFrame;if("undefined"!=typeof Zone&&t&&e){const n=t[Zone.__symbol__("OriginalDelegate")];n&&(t=n);const r=e[Zone.__symbol__("OriginalDelegate")];r&&(e=r)}return{nativeRequestAnimationFrame:t,nativeCancelAnimationFrame:e}}().nativeRequestAnimationFrame,function(t){const e=()=>{!function(t){-1===t.lastRequestAnimationFrameId&&(t.lastRequestAnimationFrameId=t.nativeRequestAnimationFrame.call(St,()=>{t.fakeTopEventTask||(t.fakeTopEventTask=Zone.root.scheduleEventTask("fakeTopEventTask",()=>{t.lastRequestAnimationFrameId=-1,Hi(t),Ni(t)},void 0,()=>{},()=>{})),t.fakeTopEventTask.invoke()}),Hi(t))}(t)};t._inner=t._inner.fork({name:"angular",properties:{isAngularZone:!0},onInvokeTask:(n,r,s,o,i,l)=>{try{return Ri(t),n.invokeTask(s,o,i,l)}finally{(t.shouldCoalesceEventChangeDetection&&"eventTask"===o.type||t.shouldCoalesceRunChangeDetection)&&e(),Fi(t)}},onInvoke:(n,r,s,o,i,l,u)=>{try{return Ri(t),n.invoke(s,o,i,l,u)}finally{t.shouldCoalesceRunChangeDetection&&e(),Fi(t)}},onHasTask:(e,n,r,s)=>{e.hasTask(r,s),n===r&&("microTask"==s.change?(t._hasPendingMicrotasks=s.microTask,Hi(t),Ni(t)):"macroTask"==s.change&&(t.hasPendingMacrotasks=s.macroTask))},onHandleError:(e,n,r,s)=>(e.handleError(r,s),t.runOutsideAngular(()=>t.onError.emit(s)),!1)})}(this)}static isInAngularZone(){return!0===Zone.current.get("isAngularZone")}static assertInAngularZone(){if(!ji.isInAngularZone())throw new Error("Expected to be in Angular Zone, but it is not!")}static assertNotInAngularZone(){if(ji.isInAngularZone())throw new Error("Expected to not be in Angular Zone, but it is!")}run(t,e,n){return this._inner.run(t,e,n)}runTask(t,e,n,r){const s=this._inner,o=s.scheduleEventTask("NgZoneEvent: "+r,t,Mi,xo,xo);try{return s.runTask(o,e,n)}finally{s.cancelTask(o)}}runGuarded(t,e,n){return this._inner.runGuarded(t,e,n)}runOutsideAngular(t){return this._outer.run(t)}}const Mi={};function Ni(t){if(0==t._nesting&&!t.hasPendingMicrotasks&&!t.isStable)try{t._nesting++,t.onMicrotaskEmpty.emit(null)}finally{if(t._nesting--,!t.hasPendingMicrotasks)try{t.runOutsideAngular(()=>t.onStable.emit(null))}finally{t.isStable=!0}}}function Hi(t){t.hasPendingMicrotasks=!!(t._hasPendingMicrotasks||(t.shouldCoalesceEventChangeDetection||t.shouldCoalesceRunChangeDetection)&&-1!==t.lastRequestAnimationFrameId)}function Ri(t){t._nesting++,t.isStable&&(t.isStable=!1,t.onUnstable.emit(null))}function Fi(t){t._nesting--,Ni(t)}class Li{constructor(){this.hasPendingMicrotasks=!1,this.hasPendingMacrotasks=!1,this.isStable=!0,this.onUnstable=new di,this.onMicrotaskEmpty=new di,this.onStable=new di,this.onError=new di}run(t,e,n){return t.apply(e,n)}runGuarded(t,e,n){return t.apply(e,n)}runOutsideAngular(t){return t()}runTask(t,e,n,r){return t.apply(e,n)}}let Bi=(()=>{class t{constructor(t){this._ngZone=t,this._pendingCount=0,this._isZoneStable=!0,this._didWork=!1,this._callbacks=[],this.taskTrackingZone=null,this._watchAngularEvents(),t.run(()=>{this.taskTrackingZone="undefined"==typeof Zone?null:Zone.current.get("TaskTrackingZone")})}_watchAngularEvents(){this._ngZone.onUnstable.subscribe({next:()=>{this._didWork=!0,this._isZoneStable=!1}}),this._ngZone.runOutsideAngular(()=>{this._ngZone.onStable.subscribe({next:()=>{ji.assertNotInAngularZone(),Pi(()=>{this._isZoneStable=!0,this._runCallbacksIfReady()})}})})}increasePendingRequestCount(){return this._pendingCount+=1,this._didWork=!0,this._pendingCount}decreasePendingRequestCount(){if(this._pendingCount-=1,this._pendingCount<0)throw new Error("pending async requests below zero");return this._runCallbacksIfReady(),this._pendingCount}isStable(){return this._isZoneStable&&0===this._pendingCount&&!this._ngZone.hasPendingMacrotasks}_runCallbacksIfReady(){if(this.isStable())Pi(()=>{for(;0!==this._callbacks.length;){let t=this._callbacks.pop();clearTimeout(t.timeoutId),t.doneCb(this._didWork)}this._didWork=!1});else{let t=this.getPendingTasks();this._callbacks=this._callbacks.filter(e=>!e.updateCb||!e.updateCb(t)||(clearTimeout(e.timeoutId),!1)),this._didWork=!0}}getPendingTasks(){return this.taskTrackingZone?this.taskTrackingZone.macroTasks.map(t=>({source:t.source,creationLocation:t.creationLocation,data:t.data})):[]}addCallback(t,e,n){let r=-1;e&&e>0&&(r=setTimeout(()=>{this._callbacks=this._callbacks.filter(t=>t.timeoutId!==r),t(this._didWork,this.getPendingTasks())},e)),this._callbacks.push({doneCb:t,timeoutId:r,updateCb:n})}whenStable(t,e,n){if(n&&!this.taskTrackingZone)throw new Error('Task tracking zone is required when passing an update callback to whenStable(). Is "zone.js/dist/task-tracking.js" loaded?');this.addCallback(t,e,n),this._runCallbacksIfReady()}getPendingRequestCount(){return this._pendingCount}findProviders(t,e,n){return[]}}return t.\u0275fac=function(e){return new(e||t)(Hn(ji))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})(),Ui=(()=>{class t{constructor(){this._applications=new Map,zi.addToWindow(this)}registerApplication(t,e){this._applications.set(t,e)}unregisterApplication(t){this._applications.delete(t)}unregisterAllApplications(){this._applications.clear()}getTestability(t){return this._applications.get(t)||null}getAllTestabilities(){return Array.from(this._applications.values())}getAllRootElements(){return Array.from(this._applications.keys())}findTestabilityInTree(t,e=!0){return zi.findTestabilityInTree(this,t,e)}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();class Zi{addToWindow(t){}findTestabilityInTree(t,e,n){return null}}let $i,zi=new Zi,Gi=!0,Wi=!1;
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Qi=new kn("AllowMultipleToken");function qi(t,e,n=[]){const r=`Platform: ${e}`,s=new kn(r);return(e=[])=>{let o=Ji();if(!o||o.injector.get(Qi,!1))if(t)t(n.concat(e).concat({provide:s,useValue:!0}));else{const t=n.concat(e).concat({provide:s,useValue:!0},{provide:ys,useValue:"platform"});!function(t){if($i&&!$i.destroyed&&!$i.injector.get(Qi,!1))throw new Error("There can be only one platform. Destroy the previous one to create a new one.");$i=t.get(Ki);const e=t.get(mi,null);e&&e.forEach(t=>t())}(Os.create({providers:t,name:r}))}return function(t){const e=Ji();if(!e)throw new Error("No platform exists!");if(!e.injector.get(t,null))throw new Error("A platform with a different configuration has been created. Please destroy it first.");return e}(s)}}function Ji(){return $i&&!$i.destroyed?$i:null}let Ki=(()=>{class t{constructor(t){this._injector=t,this._modules=[],this._destroyListeners=[],this._destroyed=!1}bootstrapModuleFactory(t,e){const n=function(t,e){let n;return n="noop"===t?new Li:("zone.js"===t?void 0:t)||new ji({enableLongStackTrace:(Wi=!0,Gi),shouldCoalesceEventChangeDetection:!!(null==e?void 0:e.ngZoneEventCoalescing),shouldCoalesceRunChangeDetection:!!(null==e?void 0:e.ngZoneRunCoalescing)}),n}(e?e.ngZone:void 0,{ngZoneEventCoalescing:e&&e.ngZoneEventCoalescing||!1,ngZoneRunCoalescing:e&&e.ngZoneRunCoalescing||!1}),r=[{provide:ji,useValue:n}];return n.run(()=>{const e=Os.create({providers:r,parent:this.injector,name:t.moduleType.name}),s=t.create(e),o=s.injector.get(Gn,null);if(!o)throw new Error("No ErrorHandler. Is platform module (BrowserModule) included?");return n.runOutsideAngular(()=>{const t=n.onError.subscribe({next:t=>{o.handleError(t)}});s.onDestroy(()=>{tl(this._modules,s),t.unsubscribe()})}),function(t,e,n){try{const r=n();return Qs(r)?r.catch(n=>{throw e.runOutsideAngular(()=>t.handleError(n)),n}):r}catch(r){throw e.runOutsideAngular(()=>t.handleError(r)),r}}(o,n,()=>{const t=s.injector.get(pi);return t.runInitializers(),t.donePromise.then(()=>(po(s.injector.get(Ci,"en-US")||"en-US"),this._moduleDoBootstrap(s),s))})})}bootstrapModule(t,e=[]){const n=Yi({},e);return function(t,e,n){const r=new ci(n);return Promise.resolve(r)}(0,0,t).then(t=>this.bootstrapModuleFactory(t,n))}_moduleDoBootstrap(t){const e=t.injector.get(Xi);if(t._bootstrapComponents.length>0)t._bootstrapComponents.forEach(t=>e.bootstrap(t));else{if(!t.instance.ngDoBootstrap)throw new Error(`The module ${K(t.instance.constructor)} was bootstrapped, but it does not declare "@NgModule.bootstrap" components nor a "ngDoBootstrap" method. Please define one of these.`);t.instance.ngDoBootstrap(e)}this._modules.push(t)}onDestroy(t){this._destroyListeners.push(t)}get injector(){return this._injector}destroy(){if(this._destroyed)throw new Error("The platform has already been destroyed!");this._modules.slice().forEach(t=>t.destroy()),this._destroyListeners.forEach(t=>t()),this._destroyed=!0}get destroyed(){return this._destroyed}}return t.\u0275fac=function(e){return new(e||t)(Hn(Os))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();function Yi(t,e){return Array.isArray(e)?e.reduce(Yi,t):Object.assign(Object.assign({},t),e)}let Xi=(()=>{class t{constructor(t,e,n,r,s){this._zone=t,this._injector=e,this._exceptionHandler=n,this._componentFactoryResolver=r,this._initStatus=s,this._bootstrapListeners=[],this._views=[],this._runningTick=!1,this._stable=!0,this.componentTypes=[],this.components=[],this._onMicrotaskEmptySubscription=this._zone.onMicrotaskEmpty.subscribe({next:()=>{this._zone.run(()=>{this.tick()})}});const o=new m(t=>{this._stable=this._zone.isStable&&!this._zone.hasPendingMacrotasks&&!this._zone.hasPendingMicrotasks,this._zone.runOutsideAngular(()=>{t.next(this._stable),t.complete()})}),i=new m(t=>{let e;this._zone.runOutsideAngular(()=>{e=this._zone.onStable.subscribe(()=>{ji.assertNotInAngularZone(),Pi(()=>{this._stable||this._zone.hasPendingMacrotasks||this._zone.hasPendingMicrotasks||(this._stable=!0,t.next(!0))})})});const n=this._zone.onUnstable.subscribe(()=>{ji.assertInAngularZone(),this._stable&&(this._stable=!1,this._zone.runOutsideAngular(()=>{t.next(!1)}))});return()=>{e.unsubscribe(),n.unsubscribe()}});this.isStable=B(o,i.pipe(t=>{return U()((e=Q,function(t){let n;n="function"==typeof e?e:function(){return e};const r=Object.create(t,G);return r.source=t,r.subjectFactory=n,r})(t));var e}))}bootstrap(t,e){if(!this._initStatus.done)throw new Error("Cannot bootstrap as there are still asynchronous initializers running. Bootstrap components in the `ngDoBootstrap` method of the root module.");let n;n=t instanceof Eo?t:this._componentFactoryResolver.resolveComponentFactory(t),this.componentTypes.push(n.componentType);const r=n.isBoundToModule?void 0:this._injector.get(ei),s=n.create(Os.NULL,[],e||n.selector,r),o=s.location.nativeElement,i=s.injector.get(Bi,null),l=i&&s.injector.get(Ui);return i&&l&&l.registerApplication(o,i),s.onDestroy(()=>{this.detachView(s.hostView),tl(this.components,s),l&&l.unregisterApplication(o)}),this._loadComponent(s),s}tick(){if(this._runningTick)throw new Error("ApplicationRef.tick is called recursively");try{this._runningTick=!0;for(let t of this._views)t.detectChanges()}catch(t){this._zone.runOutsideAngular(()=>this._exceptionHandler.handleError(t))}finally{this._runningTick=!1}}attachView(t){const e=t;this._views.push(e),e.attachToAppRef(this)}detachView(t){const e=t;tl(this._views,e),e.detachFromAppRef()}_loadComponent(t){this.attachView(t.hostView),this.tick(),this.components.push(t),this._injector.get(bi,[]).concat(this._bootstrapListeners).forEach(e=>e(t))}ngOnDestroy(){this._views.slice().forEach(t=>t.destroy()),this._onMicrotaskEmptySubscription.unsubscribe()}get viewCount(){return this._views.length}}return t.\u0275fac=function(e){return new(e||t)(Hn(ji),Hn(Os),Hn(Gn),Hn(ko),Hn(pi))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();function tl(t,e){const n=t.indexOf(e);n>-1&&t.splice(n,1)}const el=qi(null,"core",[{provide:vi,useValue:"unknown"},{provide:Ki,deps:[Os]},{provide:Ui,deps:[]},{provide:wi,deps:[]}]),nl=[{provide:Xi,useClass:Xi,deps:[ji,Os,Gn,ko,pi]},{provide:oi,deps:[ji],useFactory:function(t){let e=[];return t.onStable.subscribe(()=>{for(;e.length;)e.pop()()}),function(t){e.push(t)}}},{provide:pi,useClass:pi,deps:[[new Bn,fi]]},{provide:Oi,useClass:Oi,deps:[]},gi,{provide:Wo,useFactory:
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
function(){return Xo},deps:[]},{provide:qo,useFactory:function(){return ti},deps:[]},{provide:Ci,useFactory:function(t){return po(t=t||"undefined"!=typeof $localize&&$localize.locale||"en-US"),t},deps:[[new Ln(Ci),new Bn,new Un]]},{provide:Ei,useValue:"USD"}];let rl=(()=>{class t{constructor(t){}}return t.\u0275fac=function(e){return new(e||t)(Hn(Xi))},t.\u0275mod=Zt({type:t}),t.\u0275inj=ut({providers:nl}),t})(),sl=null;function ol(){return sl}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const il=new kn("DocumentToken");
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */var ll=function(t){return t[t.Zero=0]="Zero",t[t.One=1]="One",t[t.Two=2]="Two",t[t.Few=3]="Few",t[t.Many=4]="Many",t[t.Other=5]="Other",t}({});
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ul{}let al=(()=>{class t extends ul{constructor(t){super(),this.locale=t}getPluralCategory(t,e){switch(function(t){return function(t){const e=function(t){return t.toLowerCase().replace(/_/g,"-")}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */(t);let n=co(e);if(n)return n;const r=e.split("-")[0];if(n=co(r),n)return n;if("en"===r)return uo;throw new Error(`Missing locale data for the locale "${t}".`)}(t)[ho.PluralCase]}(e||this.locale)(t)){case ll.Zero:return"zero";case ll.One:return"one";case ll.Two:return"two";case ll.Few:return"few";case ll.Many:return"many";default:return"other"}}}return t.\u0275fac=function(e){return new(e||t)(Hn(Ci))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})(),cl=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=Zt({type:t}),t.\u0275inj=ut({providers:[{provide:ul,useClass:al}]}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class hl extends
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license Angular v11.2.11
 * (c) 2010-2021 Google LLC. https://angular.io/
 * License: MIT
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
class extends class{}{constructor(){super()}supportsDOMEvents(){return!0}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */{static makeCurrent(){var t;t=new hl,sl||(sl=t)}getProperty(t,e){return t[e]}log(t){window.console&&window.console.log&&window.console.log(t)}logGroup(t){window.console&&window.console.group&&window.console.group(t)}logGroupEnd(){window.console&&window.console.groupEnd&&window.console.groupEnd()}onAndCancel(t,e,n){return t.addEventListener(e,n,!1),()=>{t.removeEventListener(e,n,!1)}}dispatchEvent(t,e){t.dispatchEvent(e)}remove(t){return t.parentNode&&t.parentNode.removeChild(t),t}getValue(t){return t.value}createElement(t,e){return(e=e||this.getDefaultDocument()).createElement(t)}createHtmlDocument(){return document.implementation.createHTMLDocument("fakeTitle")}getDefaultDocument(){return document}isElementNode(t){return t.nodeType===Node.ELEMENT_NODE}isShadowRoot(t){return t instanceof DocumentFragment}getGlobalEventTarget(t,e){return"window"===e?window:"document"===e?t:"body"===e?t.body:null}getHistory(){return window.history}getLocation(){return window.location}getBaseHref(t){const e=fl||(fl=document.querySelector("base"),fl)?fl.getAttribute("href"):null;return null==e?null:(n=e,dl||(dl=document.createElement("a")),dl.setAttribute("href",n),"/"===dl.pathname.charAt(0)?dl.pathname:"/"+dl.pathname);var n;
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}resetBaseElement(){fl=null}getUserAgent(){return window.navigator.userAgent}performanceNow(){return window.performance&&window.performance.now?window.performance.now():(new Date).getTime()}supportsCookies(){return!0}getCookie(t){
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
return function(t,e){e=encodeURIComponent(e);for(const n of t.split(";")){const t=n.indexOf("="),[r,s]=-1==t?[n,""]:[n.slice(0,t),n.slice(t+1)];if(r.trim()===e)return decodeURIComponent(s)}return null}(document.cookie,t)}}let dl,fl=null;const pl=new kn("TRANSITION_ID"),_l=[{provide:fi,useFactory:function(t,e,n){return()=>{n.get(pi).donePromise.then(()=>{const n=ol();Array.prototype.slice.apply(e.querySelectorAll("style[ng-transition]")).filter(e=>e.getAttribute("ng-transition")===t).forEach(t=>n.remove(t))})}},deps:[pl,il,Os],multi:!0}];
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class gl{static init(){var t;t=new gl,zi=t}addToWindow(t){St.getAngularTestability=(e,n=!0)=>{const r=t.findTestabilityInTree(e,n);if(null==r)throw new Error("Could not find testability for element.");return r},St.getAllAngularTestabilities=()=>t.getAllTestabilities(),St.getAllAngularRootElements=()=>t.getAllRootElements(),St.frameworkStabilizers||(St.frameworkStabilizers=[]),St.frameworkStabilizers.push(t=>{const e=St.getAllAngularTestabilities();let n=e.length,r=!1;const s=function(e){r=r||e,n--,0==n&&t(r)};e.forEach(function(t){t.whenStable(s)})})}findTestabilityInTree(t,e,n){if(null==e)return null;const r=t.getTestability(e);return null!=r?r:n?ol().isShadowRoot(e)?this.findTestabilityInTree(t,e.host,!0):this.findTestabilityInTree(t,e.parentElement,!0):null}}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const yl=new kn("EventManagerPlugins");let ml=(()=>{class t{constructor(t,e){this._zone=e,this._eventNameToPlugin=new Map,t.forEach(t=>t.manager=this),this._plugins=t.slice().reverse()}addEventListener(t,e,n){return this._findPluginFor(e).addEventListener(t,e,n)}addGlobalEventListener(t,e,n){return this._findPluginFor(e).addGlobalEventListener(t,e,n)}getZone(){return this._zone}_findPluginFor(t){const e=this._eventNameToPlugin.get(t);if(e)return e;const n=this._plugins;for(let r=0;r<n.length;r++){const e=n[r];if(e.supports(t))return this._eventNameToPlugin.set(t,e),e}throw new Error(`No event manager plugin found for event ${t}`)}}return t.\u0275fac=function(e){return new(e||t)(Hn(yl),Hn(ji))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();class vl{constructor(t){this._doc=t}addGlobalEventListener(t,e,n){const r=ol().getGlobalEventTarget(this._doc,t);if(!r)throw new Error(`Unsupported event target ${r} for event ${e}`);return this.addEventListener(r,e,n)}}let bl=(()=>{class t{constructor(){this._stylesSet=new Set}addStyles(t){const e=new Set;t.forEach(t=>{this._stylesSet.has(t)||(this._stylesSet.add(t),e.add(t))}),this.onStylesAdded(e)}onStylesAdded(t){}getAllStyles(){return Array.from(this._stylesSet)}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})(),wl=(()=>{class t extends bl{constructor(t){super(),this._doc=t,this._hostNodes=new Set,this._styleNodes=new Set,this._hostNodes.add(t.head)}_addStylesToHost(t,e){t.forEach(t=>{const n=this._doc.createElement("style");n.textContent=t,this._styleNodes.add(e.appendChild(n))})}addHost(t){this._addStylesToHost(this._stylesSet,t),this._hostNodes.add(t)}removeHost(t){this._hostNodes.delete(t)}onStylesAdded(t){this._hostNodes.forEach(e=>this._addStylesToHost(t,e))}ngOnDestroy(){this._styleNodes.forEach(t=>ol().remove(t))}}return t.\u0275fac=function(e){return new(e||t)(Hn(il))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Cl={svg:"http://www.w3.org/2000/svg",xhtml:"http://www.w3.org/1999/xhtml",xlink:"http://www.w3.org/1999/xlink",xml:"http://www.w3.org/XML/1998/namespace",xmlns:"http://www.w3.org/2000/xmlns/"},El=/%COMP%/g;function Al(t,e,n){for(let r=0;r<e.length;r++){let s=e[r];Array.isArray(s)?Al(t,s,n):(s=s.replace(El,t),n.push(s))}return n}function kl(t){return e=>{if("__ngUnwrap__"===e)return t;!1===t(e)&&(e.preventDefault(),e.returnValue=!1)}}let xl=(()=>{class t{constructor(t,e,n){this.eventManager=t,this.sharedStylesHost=e,this.appId=n,this.rendererByCompId=new Map,this.defaultRenderer=new Sl(t)}createRenderer(t,e){if(!t||!e)return this.defaultRenderer;switch(e.encapsulation){case Ct.Emulated:{let n=this.rendererByCompId.get(e.id);return n||(n=new Tl(this.eventManager,this.sharedStylesHost,e,this.appId),this.rendererByCompId.set(e.id,n)),n.applyToHost(t),n}case 1:case Ct.ShadowDom:return new Il(this.eventManager,this.sharedStylesHost,t,e);default:if(!this.rendererByCompId.has(e.id)){const t=Al(e.id,e.styles,[]);this.sharedStylesHost.addStyles(t),this.rendererByCompId.set(e.id,this.defaultRenderer)}return this.defaultRenderer}}begin(){}end(){}}return t.\u0275fac=function(e){return new(e||t)(Hn(ml),Hn(wl),Hn(_i))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();class Sl{constructor(t){this.eventManager=t,this.data=Object.create(null)}destroy(){}createElement(t,e){return e?document.createElementNS(Cl[e]||e,t):document.createElement(t)}createComment(t){return document.createComment(t)}createText(t){return document.createTextNode(t)}appendChild(t,e){t.appendChild(e)}insertBefore(t,e,n){t&&t.insertBefore(e,n)}removeChild(t,e){t&&t.removeChild(e)}selectRootElement(t,e){let n="string"==typeof t?document.querySelector(t):t;if(!n)throw new Error(`The selector "${t}" did not match any elements`);return e||(n.textContent=""),n}parentNode(t){return t.parentNode}nextSibling(t){return t.nextSibling}setAttribute(t,e,n,r){if(r){e=r+":"+e;const s=Cl[r];s?t.setAttributeNS(s,e,n):t.setAttribute(e,n)}else t.setAttribute(e,n)}removeAttribute(t,e,n){if(n){const r=Cl[n];r?t.removeAttributeNS(r,e):t.removeAttribute(`${n}:${e}`)}else t.removeAttribute(e)}addClass(t,e){t.classList.add(e)}removeClass(t,e){t.classList.remove(e)}setStyle(t,e,n,r){r&(Jn.DashCase|Jn.Important)?t.style.setProperty(e,n,r&Jn.Important?"important":""):t.style[e]=n}removeStyle(t,e,n){n&Jn.DashCase?t.style.removeProperty(e):t.style[e]=""}setProperty(t,e,n){t[e]=n}setValue(t,e){t.nodeValue=e}listen(t,e,n){return"string"==typeof t?this.eventManager.addGlobalEventListener(t,e,kl(n)):this.eventManager.addEventListener(t,e,kl(n))}}class Tl extends Sl{constructor(t,e,n,r){super(t),this.component=n;const s=Al(r+"-"+n.id,n.styles,[]);e.addStyles(s),this.contentAttr="_ngcontent-%COMP%".replace(El,r+"-"+n.id),this.hostAttr="_nghost-%COMP%".replace(El,r+"-"+n.id)}applyToHost(t){super.setAttribute(t,this.hostAttr,"")}createElement(t,e){const n=super.createElement(t,e);return super.setAttribute(n,this.contentAttr,""),n}}class Il extends Sl{constructor(t,e,n,r){super(t),this.sharedStylesHost=e,this.hostEl=n,this.shadowRoot=n.attachShadow({mode:"open"}),this.sharedStylesHost.addHost(this.shadowRoot);const s=Al(r.id,r.styles,[]);for(let o=0;o<s.length;o++){const t=document.createElement("style");t.textContent=s[o],this.shadowRoot.appendChild(t)}}nodeOrShadowRoot(t){return t===this.hostEl?this.shadowRoot:t}destroy(){this.sharedStylesHost.removeHost(this.shadowRoot)}appendChild(t,e){return super.appendChild(this.nodeOrShadowRoot(t),e)}insertBefore(t,e,n){return super.insertBefore(this.nodeOrShadowRoot(t),e,n)}removeChild(t,e){return super.removeChild(this.nodeOrShadowRoot(t),e)}parentNode(t){return this.nodeOrShadowRoot(super.parentNode(this.nodeOrShadowRoot(t)))}}let Vl=(()=>{class t extends vl{constructor(t){super(t)}supports(t){return!0}addEventListener(t,e,n){return t.addEventListener(e,n,!1),()=>this.removeEventListener(t,e,n)}removeEventListener(t,e,n){return t.removeEventListener(e,n)}}return t.\u0275fac=function(e){return new(e||t)(Hn(il))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const Ol=["alt","control","meta","shift"],Dl={"\b":"Backspace","\t":"Tab","\x7f":"Delete","\x1b":"Escape",Del:"Delete",Esc:"Escape",Left:"ArrowLeft",Right:"ArrowRight",Up:"ArrowUp",Down:"ArrowDown",Menu:"ContextMenu",Scroll:"ScrollLock",Win:"OS"},Pl={A:"1",B:"2",C:"3",D:"4",E:"5",F:"6",G:"7",H:"8",I:"9",J:"*",K:"+",M:"-",N:".",O:"/","`":"0","\x90":"NumLock"},jl={alt:t=>t.altKey,control:t=>t.ctrlKey,meta:t=>t.metaKey,shift:t=>t.shiftKey};let Ml=(()=>{class t extends vl{constructor(t){super(t)}supports(e){return null!=t.parseEventName(e)}addEventListener(e,n,r){const s=t.parseEventName(n),o=t.eventCallback(s.fullKey,r,this.manager.getZone());return this.manager.getZone().runOutsideAngular(()=>ol().onAndCancel(e,s.domEventName,o))}static parseEventName(e){const n=e.toLowerCase().split("."),r=n.shift();if(0===n.length||"keydown"!==r&&"keyup"!==r)return null;const s=t._normalizeKey(n.pop());let o="";if(Ol.forEach(t=>{const e=n.indexOf(t);e>-1&&(n.splice(e,1),o+=t+".")}),o+=s,0!=n.length||0===s.length)return null;const i={};return i.domEventName=r,i.fullKey=o,i}static getEventFullKey(t){let e="",n=function(t){let e=t.key;if(null==e){if(e=t.keyIdentifier,null==e)return"Unidentified";e.startsWith("U+")&&(e=String.fromCharCode(parseInt(e.substring(2),16)),3===t.location&&Pl.hasOwnProperty(e)&&(e=Pl[e]))}return Dl[e]||e}(t);return n=n.toLowerCase()," "===n?n="space":"."===n&&(n="dot"),Ol.forEach(r=>{r!=n&&(0,jl[r])(t)&&(e+=r+".")}),e+=n,e}static eventCallback(e,n,r){return s=>{t.getEventFullKey(s)===e&&r.runGuarded(()=>n(s))}}static _normalizeKey(t){switch(t){case"esc":return"escape";default:return t}}}return t.\u0275fac=function(e){return new(e||t)(Hn(il))},t.\u0275prov=lt({token:t,factory:t.\u0275fac}),t})();const Nl=qi(el,"browser",[{provide:vi,useValue:"browser"},{provide:mi,useValue:
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
function(){hl.makeCurrent(),gl.init()},multi:!0},{provide:il,useFactory:function(){return function(t){le=t}(document),document},deps:[]}]),Hl=[[],{provide:ys,useValue:"root"},{provide:Gn,useFactory:function(){return new Gn},deps:[]},{provide:yl,useClass:Vl,multi:!0,deps:[il,ji,vi]},{provide:yl,useClass:Ml,multi:!0,deps:[il]},[],{provide:xl,useClass:xl,deps:[ml,wl,_i]},{provide:Vo,useExisting:xl},{provide:bl,useExisting:wl},{provide:wl,useClass:wl,deps:[il]},{provide:Bi,useClass:Bi,deps:[ji]},{provide:ml,useClass:ml,deps:[yl,ji]},[]];let Rl=(()=>{class t{constructor(t){if(t)throw new Error("BrowserModule has already been loaded. If you need access to common directives such as NgIf and NgFor from a lazy loaded module, import CommonModule instead.")}static withServerTransition(e){return{ngModule:t,providers:[{provide:_i,useValue:e.appId},{provide:pl,useExisting:_i},_l]}}}return t.\u0275fac=function(e){return new(e||t)(Hn(t,12))},t.\u0275mod=Zt({type:t}),t.\u0275inj=ut({providers:Hl,imports:[cl,rl]}),t})();function Fl(t,e){return new m(n=>{const r=t.length;if(0===r)return void n.complete();const s=new Array(r);let o=0,i=0;for(let l=0;l<r;l++){const u=M(t[l]);let a=!1;n.add(u.subscribe({next:t=>{a||(a=!0,i++),s[l]=t},error:t=>n.error(t),complete:()=>{o++,o!==r&&a||(i===r&&n.next(e?e.reduce((t,e,n)=>(t[e]=s[n],t),{}):s),n.complete())}}))}})}
/**
 * @license Angular v11.2.11
 * (c) 2010-2021 Google LLC. https://angular.io/
 * License: MIT
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */"undefined"!=typeof window&&window;const Ll=new kn("NgValueAccessor"),Bl={provide:Ll,useExisting:tt(()=>Zl),multi:!0},Ul=new kn("CompositionEventMode");
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */let Zl=(()=>{class t{constructor(t,e,n){this._renderer=t,this._elementRef=e,this._compositionMode=n,this.onChange=t=>{},this.onTouched=()=>{},this._composing=!1,null==this._compositionMode&&(this._compositionMode=!function(){const t=ol()?ol().getUserAgent():"";return/android (\d+)/.test(t.toLowerCase())}())}writeValue(t){this._renderer.setProperty(this._elementRef.nativeElement,"value",null==t?"":t)}registerOnChange(t){this.onChange=t}registerOnTouched(t){this.onTouched=t}setDisabledState(t){this._renderer.setProperty(this._elementRef.nativeElement,"disabled",t)}_handleInput(t){(!this._compositionMode||this._compositionMode&&!this._composing)&&this.onChange(t)}_compositionStart(){this._composing=!0}_compositionEnd(t){this._composing=!1,this._compositionMode&&this.onChange(t)}}return t.\u0275fac=function(e){return new(e||t)(Zs(Oo),Zs(Io),Zs(Ul,8))},t.\u0275dir=zt({type:t,selectors:[["input","formControlName","",3,"type","checkbox"],["textarea","formControlName",""],["input","formControl","",3,"type","checkbox"],["textarea","formControl",""],["input","ngModel","",3,"type","checkbox"],["textarea","ngModel",""],["","ngDefaultControl",""]],hostBindings:function(t,e){1&t&&qs("input",function(t){return e._handleInput(t.target.value)})("blur",function(){return e.onTouched()})("compositionstart",function(){return e._compositionStart()})("compositionend",function(t){return e._compositionEnd(t.target.value)})},features:[Co([Bl])]}),t})();
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */const $l=new kn("NgValidators"),zl=new kn("NgAsyncValidators");function Gl(t){return null!=t}function Wl(t){const e=Qs(t)?M(t):t;return e,e}function Ql(t){let e={};return t.forEach(t=>{e=null!=t?Object.assign(Object.assign({},e),t):e}),0===Object.keys(e).length?null:e}function ql(t,e){return e.map(e=>e(t))}function Jl(t){return t.map(t=>function(t){return!t.validate}(t)?t:e=>t.validate(e))}function Kl(t){return null!=t?function(t){if(!t)return null;const e=t.filter(Gl);return 0==e.length?null:function(t){return Ql(ql(t,e))}}(Jl(t)):null}function Yl(t){return null!=t?function(t){if(!t)return null;const e=t.filter(Gl);return 0==e.length?null:function(t){
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
return function(...t){if(1===t.length){const e=t[0];if(u(e))return Fl(e,null);if(a(e)&&Object.getPrototypeOf(e)===Object.prototype){const t=Object.keys(e);return Fl(t.map(t=>e[t]),t)}}if("function"==typeof t[t.length-1]){const e=t.pop();return Fl(t=1===t.length&&u(t[0])?t[0]:t,null).pipe(k(t=>e(...t)))}return Fl(t,null)}(ql(t,e).map(Wl)).pipe(k(Ql))}}(Jl(t)):null}function Xl(t,e){return null===t?[e]:Array.isArray(t)?[...t,e]:[t,e]}let tu=(()=>{class t{constructor(){this._rawValidators=[],this._rawAsyncValidators=[],this._onDestroyCallbacks=[]}get value(){return this.control?this.control.value:null}get valid(){return this.control?this.control.valid:null}get invalid(){return this.control?this.control.invalid:null}get pending(){return this.control?this.control.pending:null}get disabled(){return this.control?this.control.disabled:null}get enabled(){return this.control?this.control.enabled:null}get errors(){return this.control?this.control.errors:null}get pristine(){return this.control?this.control.pristine:null}get dirty(){return this.control?this.control.dirty:null}get touched(){return this.control?this.control.touched:null}get status(){return this.control?this.control.status:null}get untouched(){return this.control?this.control.untouched:null}get statusChanges(){return this.control?this.control.statusChanges:null}get valueChanges(){return this.control?this.control.valueChanges:null}get path(){return null}_setValidators(t){this._rawValidators=t||[],this._composedValidatorFn=Kl(this._rawValidators)}_setAsyncValidators(t){this._rawAsyncValidators=t||[],this._composedAsyncValidatorFn=Yl(this._rawAsyncValidators)}get validator(){return this._composedValidatorFn||null}get asyncValidator(){return this._composedAsyncValidatorFn||null}_registerOnDestroy(t){this._onDestroyCallbacks.push(t)}_invokeOnDestroyCallbacks(){this._onDestroyCallbacks.forEach(t=>t()),this._onDestroyCallbacks=[]}reset(t){this.control&&this.control.reset(t)}hasError(t,e){return!!this.control&&this.control.hasError(t,e)}getError(t,e){return this.control?this.control.getError(t,e):null}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275dir=zt({type:t}),t})(),eu=(()=>{class t extends tu{get formDirective(){return null}get path(){return null}}return t.\u0275fac=function(e){return nu(e||t)},t.\u0275dir=zt({type:t,features:[Ps]}),t})();const nu=Cn(eu);
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */class ru extends tu{constructor(){super(...arguments),this._parent=null,this.name=null,this.valueAccessor=null
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}}let su=(()=>{class t extends class{constructor(t){this._cd=t}is(t){var e,n;return!!(null===(n=null===(e=this._cd)||void 0===e?void 0:e.control)||void 0===n?void 0:n[t])}}{constructor(t){super(t)}}return t.\u0275fac=function(e){return new(e||t)(Zs(ru,2))},t.\u0275dir=zt({type:t,selectors:[["","formControlName",""],["","ngModel",""],["","formControl",""]],hostVars:14,hostBindings:function(t,e){2&t&&to("ng-untouched",e.is("untouched"))("ng-touched",e.is("touched"))("ng-pristine",e.is("pristine"))("ng-dirty",e.is("dirty"))("ng-valid",e.is("valid"))("ng-invalid",e.is("invalid"))("ng-pending",e.is("pending"))},features:[Ps]}),t})();function ou(t,e){t.forEach(t=>{t.registerOnValidatorChange&&t.registerOnValidatorChange(e)})}function iu(t,e){t._pendingDirty&&t.markAsDirty(),t.setValue(t._pendingValue,{emitModelToViewChange:!1}),e.viewToModelUpdate(t._pendingValue),t._pendingChange=!1}function lu(t,e){const n=t.indexOf(e);n>-1&&t.splice(n,1)}function uu(t){return(du(t)?t.validators:t)||null}function au(t){return Array.isArray(t)?Kl(t):t||null}function cu(t,e){return(du(e)?e.asyncValidators:t)||null}function hu(t){return Array.isArray(t)?Yl(t):t||null}function du(t){return null!=t&&!Array.isArray(t)&&"object"==typeof t}class fu{constructor(t,e){this._hasOwnPendingAsyncValidator=!1,this._onCollectionChange=()=>{},this._parent=null,this.pristine=!0,this.touched=!1,this._onDisabledChange=[],this._rawValidators=t,this._rawAsyncValidators=e,this._composedValidatorFn=au(this._rawValidators),this._composedAsyncValidatorFn=hu(this._rawAsyncValidators)}get validator(){return this._composedValidatorFn}set validator(t){this._rawValidators=this._composedValidatorFn=t}get asyncValidator(){return this._composedAsyncValidatorFn}set asyncValidator(t){this._rawAsyncValidators=this._composedAsyncValidatorFn=t}get parent(){return this._parent}get valid(){return"VALID"===this.status}get invalid(){return"INVALID"===this.status}get pending(){return"PENDING"==this.status}get disabled(){return"DISABLED"===this.status}get enabled(){return"DISABLED"!==this.status}get dirty(){return!this.pristine}get untouched(){return!this.touched}get updateOn(){return this._updateOn?this._updateOn:this.parent?this.parent.updateOn:"change"}setValidators(t){this._rawValidators=t,this._composedValidatorFn=au(t)}setAsyncValidators(t){this._rawAsyncValidators=t,this._composedAsyncValidatorFn=hu(t)}clearValidators(){this.validator=null}clearAsyncValidators(){this.asyncValidator=null}markAsTouched(t={}){this.touched=!0,this._parent&&!t.onlySelf&&this._parent.markAsTouched(t)}markAllAsTouched(){this.markAsTouched({onlySelf:!0}),this._forEachChild(t=>t.markAllAsTouched())}markAsUntouched(t={}){this.touched=!1,this._pendingTouched=!1,this._forEachChild(t=>{t.markAsUntouched({onlySelf:!0})}),this._parent&&!t.onlySelf&&this._parent._updateTouched(t)}markAsDirty(t={}){this.pristine=!1,this._parent&&!t.onlySelf&&this._parent.markAsDirty(t)}markAsPristine(t={}){this.pristine=!0,this._pendingDirty=!1,this._forEachChild(t=>{t.markAsPristine({onlySelf:!0})}),this._parent&&!t.onlySelf&&this._parent._updatePristine(t)}markAsPending(t={}){this.status="PENDING",!1!==t.emitEvent&&this.statusChanges.emit(this.status),this._parent&&!t.onlySelf&&this._parent.markAsPending(t)}disable(t={}){const e=this._parentMarkedDirty(t.onlySelf);this.status="DISABLED",this.errors=null,this._forEachChild(e=>{e.disable(Object.assign(Object.assign({},t),{onlySelf:!0}))}),this._updateValue(),!1!==t.emitEvent&&(this.valueChanges.emit(this.value),this.statusChanges.emit(this.status)),this._updateAncestors(Object.assign(Object.assign({},t),{skipPristineCheck:e})),this._onDisabledChange.forEach(t=>t(!0))}enable(t={}){const e=this._parentMarkedDirty(t.onlySelf);this.status="VALID",this._forEachChild(e=>{e.enable(Object.assign(Object.assign({},t),{onlySelf:!0}))}),this.updateValueAndValidity({onlySelf:!0,emitEvent:t.emitEvent}),this._updateAncestors(Object.assign(Object.assign({},t),{skipPristineCheck:e})),this._onDisabledChange.forEach(t=>t(!1))}_updateAncestors(t){this._parent&&!t.onlySelf&&(this._parent.updateValueAndValidity(t),t.skipPristineCheck||this._parent._updatePristine(),this._parent._updateTouched())}setParent(t){this._parent=t}updateValueAndValidity(t={}){this._setInitialStatus(),this._updateValue(),this.enabled&&(this._cancelExistingSubscription(),this.errors=this._runValidator(),this.status=this._calculateStatus(),"VALID"!==this.status&&"PENDING"!==this.status||this._runAsyncValidator(t.emitEvent)),!1!==t.emitEvent&&(this.valueChanges.emit(this.value),this.statusChanges.emit(this.status)),this._parent&&!t.onlySelf&&this._parent.updateValueAndValidity(t)}_updateTreeValidity(t={emitEvent:!0}){this._forEachChild(e=>e._updateTreeValidity(t)),this.updateValueAndValidity({onlySelf:!0,emitEvent:t.emitEvent})}_setInitialStatus(){this.status=this._allControlsDisabled()?"DISABLED":"VALID"}_runValidator(){return this.validator?this.validator(this):null}_runAsyncValidator(t){if(this.asyncValidator){this.status="PENDING",this._hasOwnPendingAsyncValidator=!0;const e=Wl(this.asyncValidator(this));this._asyncValidationSubscription=e.subscribe(e=>{this._hasOwnPendingAsyncValidator=!1,this.setErrors(e,{emitEvent:t})})}}_cancelExistingSubscription(){this._asyncValidationSubscription&&(this._asyncValidationSubscription.unsubscribe(),this._hasOwnPendingAsyncValidator=!1)}setErrors(t,e={}){this.errors=t,this._updateControlsErrors(!1!==e.emitEvent)}get(t){return function(t,e,n){if(null==e)return null;if(Array.isArray(e)||(e=e.split(".")),Array.isArray(e)&&0===e.length)return null;let r=t;return e.forEach(t=>{r=r instanceof _u?r.controls.hasOwnProperty(t)?r.controls[t]:null:r instanceof gu&&r.at(t)||null}),r}(this,t)}getError(t,e){const n=e?this.get(e):this;return n&&n.errors?n.errors[t]:null}hasError(t,e){return!!this.getError(t,e)}get root(){let t=this;for(;t._parent;)t=t._parent;return t}_updateControlsErrors(t){this.status=this._calculateStatus(),t&&this.statusChanges.emit(this.status),this._parent&&this._parent._updateControlsErrors(t)}_initObservables(){this.valueChanges=new di,this.statusChanges=new di}_calculateStatus(){return this._allControlsDisabled()?"DISABLED":this.errors?"INVALID":this._hasOwnPendingAsyncValidator||this._anyControlsHaveStatus("PENDING")?"PENDING":this._anyControlsHaveStatus("INVALID")?"INVALID":"VALID"}_anyControlsHaveStatus(t){return this._anyControls(e=>e.status===t)}_anyControlsDirty(){return this._anyControls(t=>t.dirty)}_anyControlsTouched(){return this._anyControls(t=>t.touched)}_updatePristine(t={}){this.pristine=!this._anyControlsDirty(),this._parent&&!t.onlySelf&&this._parent._updatePristine(t)}_updateTouched(t={}){this.touched=this._anyControlsTouched(),this._parent&&!t.onlySelf&&this._parent._updateTouched(t)}_isBoxedValue(t){return"object"==typeof t&&null!==t&&2===Object.keys(t).length&&"value"in t&&"disabled"in t}_registerOnCollectionChange(t){this._onCollectionChange=t}_setUpdateStrategy(t){du(t)&&null!=t.updateOn&&(this._updateOn=t.updateOn)}_parentMarkedDirty(t){return!t&&!(!this._parent||!this._parent.dirty)&&!this._parent._anyControlsDirty()}}class pu extends fu{constructor(t=null,e,n){super(uu(e),cu(n,e)),this._onChange=[],this._applyFormState(t),this._setUpdateStrategy(e),this._initObservables(),this.updateValueAndValidity({onlySelf:!0,emitEvent:!!n})}setValue(t,e={}){this.value=this._pendingValue=t,this._onChange.length&&!1!==e.emitModelToViewChange&&this._onChange.forEach(t=>t(this.value,!1!==e.emitViewToModelChange)),this.updateValueAndValidity(e)}patchValue(t,e={}){this.setValue(t,e)}reset(t=null,e={}){this._applyFormState(t),this.markAsPristine(e),this.markAsUntouched(e),this.setValue(this.value,e),this._pendingChange=!1}_updateValue(){}_anyControls(t){return!1}_allControlsDisabled(){return this.disabled}registerOnChange(t){this._onChange.push(t)}_unregisterOnChange(t){lu(this._onChange,t)}registerOnDisabledChange(t){this._onDisabledChange.push(t)}_unregisterOnDisabledChange(t){lu(this._onDisabledChange,t)}_forEachChild(t){}_syncPendingControls(){return!("submit"!==this.updateOn||(this._pendingDirty&&this.markAsDirty(),this._pendingTouched&&this.markAsTouched(),!this._pendingChange)||(this.setValue(this._pendingValue,{onlySelf:!0,emitModelToViewChange:!1}),0))}_applyFormState(t){this._isBoxedValue(t)?(this.value=this._pendingValue=t.value,t.disabled?this.disable({onlySelf:!0,emitEvent:!1}):this.enable({onlySelf:!0,emitEvent:!1})):this.value=this._pendingValue=t}}class _u extends fu{constructor(t,e,n){super(uu(e),cu(n,e)),this.controls=t,this._initObservables(),this._setUpdateStrategy(e),this._setUpControls(),this.updateValueAndValidity({onlySelf:!0,emitEvent:!!n})}registerControl(t,e){return this.controls[t]?this.controls[t]:(this.controls[t]=e,e.setParent(this),e._registerOnCollectionChange(this._onCollectionChange),e)}addControl(t,e){this.registerControl(t,e),this.updateValueAndValidity(),this._onCollectionChange()}removeControl(t){this.controls[t]&&this.controls[t]._registerOnCollectionChange(()=>{}),delete this.controls[t],this.updateValueAndValidity(),this._onCollectionChange()}setControl(t,e){this.controls[t]&&this.controls[t]._registerOnCollectionChange(()=>{}),delete this.controls[t],e&&this.registerControl(t,e),this.updateValueAndValidity(),this._onCollectionChange()}contains(t){return this.controls.hasOwnProperty(t)&&this.controls[t].enabled}setValue(t,e={}){this._checkAllValuesPresent(t),Object.keys(t).forEach(n=>{this._throwIfControlMissing(n),this.controls[n].setValue(t[n],{onlySelf:!0,emitEvent:e.emitEvent})}),this.updateValueAndValidity(e)}patchValue(t,e={}){null!=t&&(Object.keys(t).forEach(n=>{this.controls[n]&&this.controls[n].patchValue(t[n],{onlySelf:!0,emitEvent:e.emitEvent})}),this.updateValueAndValidity(e))}reset(t={},e={}){this._forEachChild((n,r)=>{n.reset(t[r],{onlySelf:!0,emitEvent:e.emitEvent})}),this._updatePristine(e),this._updateTouched(e),this.updateValueAndValidity(e)}getRawValue(){return this._reduceChildren({},(t,e,n)=>(t[n]=e instanceof pu?e.value:e.getRawValue(),t))}_syncPendingControls(){let t=this._reduceChildren(!1,(t,e)=>!!e._syncPendingControls()||t);return t&&this.updateValueAndValidity({onlySelf:!0}),t}_throwIfControlMissing(t){if(!Object.keys(this.controls).length)throw new Error("\n        There are no form controls registered with this group yet. If you're using ngModel,\n        you may want to check next tick (e.g. use setTimeout).\n      ");if(!this.controls[t])throw new Error(`Cannot find form control with name: ${t}.`)}_forEachChild(t){Object.keys(this.controls).forEach(e=>{const n=this.controls[e];n&&t(n,e)})}_setUpControls(){this._forEachChild(t=>{t.setParent(this),t._registerOnCollectionChange(this._onCollectionChange)})}_updateValue(){this.value=this._reduceValue()}_anyControls(t){for(const e of Object.keys(this.controls)){const n=this.controls[e];if(this.contains(e)&&t(n))return!0}return!1}_reduceValue(){return this._reduceChildren({},(t,e,n)=>((e.enabled||this.disabled)&&(t[n]=e.value),t))}_reduceChildren(t,e){let n=t;return this._forEachChild((t,r)=>{n=e(n,t,r)}),n}_allControlsDisabled(){for(const t of Object.keys(this.controls))if(this.controls[t].enabled)return!1;return Object.keys(this.controls).length>0||this.disabled}_checkAllValuesPresent(t){this._forEachChild((e,n)=>{if(void 0===t[n])throw new Error(`Must supply a value for form control with name: '${n}'.`)})}}class gu extends fu{constructor(t,e,n){super(uu(e),cu(n,e)),this.controls=t,this._initObservables(),this._setUpdateStrategy(e),this._setUpControls(),this.updateValueAndValidity({onlySelf:!0,emitEvent:!!n})}at(t){return this.controls[t]}push(t){this.controls.push(t),this._registerControl(t),this.updateValueAndValidity(),this._onCollectionChange()}insert(t,e){this.controls.splice(t,0,e),this._registerControl(e),this.updateValueAndValidity()}removeAt(t){this.controls[t]&&this.controls[t]._registerOnCollectionChange(()=>{}),this.controls.splice(t,1),this.updateValueAndValidity()}setControl(t,e){this.controls[t]&&this.controls[t]._registerOnCollectionChange(()=>{}),this.controls.splice(t,1),e&&(this.controls.splice(t,0,e),this._registerControl(e)),this.updateValueAndValidity(),this._onCollectionChange()}get length(){return this.controls.length}setValue(t,e={}){this._checkAllValuesPresent(t),t.forEach((t,n)=>{this._throwIfControlMissing(n),this.at(n).setValue(t,{onlySelf:!0,emitEvent:e.emitEvent})}),this.updateValueAndValidity(e)}patchValue(t,e={}){null!=t&&(t.forEach((t,n)=>{this.at(n)&&this.at(n).patchValue(t,{onlySelf:!0,emitEvent:e.emitEvent})}),this.updateValueAndValidity(e))}reset(t=[],e={}){this._forEachChild((n,r)=>{n.reset(t[r],{onlySelf:!0,emitEvent:e.emitEvent})}),this._updatePristine(e),this._updateTouched(e),this.updateValueAndValidity(e)}getRawValue(){return this.controls.map(t=>t instanceof pu?t.value:t.getRawValue())}clear(){this.controls.length<1||(this._forEachChild(t=>t._registerOnCollectionChange(()=>{})),this.controls.splice(0),this.updateValueAndValidity())}_syncPendingControls(){let t=this.controls.reduce((t,e)=>!!e._syncPendingControls()||t,!1);return t&&this.updateValueAndValidity({onlySelf:!0}),t}_throwIfControlMissing(t){if(!this.controls.length)throw new Error("\n        There are no form controls registered with this array yet. If you're using ngModel,\n        you may want to check next tick (e.g. use setTimeout).\n      ");if(!this.at(t))throw new Error(`Cannot find form control at index ${t}`)}_forEachChild(t){this.controls.forEach((e,n)=>{t(e,n)})}_updateValue(){this.value=this.controls.filter(t=>t.enabled||this.disabled).map(t=>t.value)}_anyControls(t){return this.controls.some(e=>e.enabled&&t(e))}_setUpControls(){this._forEachChild(t=>this._registerControl(t))}_checkAllValuesPresent(t){this._forEachChild((e,n)=>{if(void 0===t[n])throw new Error(`Must supply a value for form control at index: ${n}.`)})}_allControlsDisabled(){for(const t of this.controls)if(t.enabled)return!1;return this.controls.length>0||this.disabled}_registerControl(t){t.setParent(this),t._registerOnCollectionChange(this._onCollectionChange)
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */}}const yu={provide:ru,useExisting:tt(()=>vu)},mu=(()=>Promise.resolve(null))();let vu=(()=>{class t extends ru{constructor(t,e,n,r){super(),this.control=new pu,this._registered=!1,this.update=new di,this._parent=t,this._setValidators(e),this._setAsyncValidators(n),this.valueAccessor=function(t,e){if(!e)return null;let n,r,s;return Array.isArray(e),e.forEach(t=>{t.constructor===Zl?n=t:Object.getPrototypeOf(t.constructor)===class{}?r=t:s=t}),s||r||n||null}(0,r)}ngOnChanges(t){this._checkForErrors(),this._registered||this._setUpControl(),"isDisabled"in t&&this._updateDisabled(t),function(t,e){if(!t.hasOwnProperty("model"))return!1;const n=t.model;return!!n.isFirstChange()||!Object.is(e,n.currentValue)}(t,this.viewModel)&&(this._updateValue(this.model),this.viewModel=this.model)}ngOnDestroy(){this.formDirective&&this.formDirective.removeControl(this)}get path(){return this._parent?[...this._parent.path,this.name]:[this.name]}get formDirective(){return this._parent?this._parent.formDirective:null}viewToModelUpdate(t){this.viewModel=t,this.update.emit(t)}_setUpControl(){this._setUpdateStrategy(),this._isStandalone()?this._setUpStandalone():this.formDirective.addControl(this),this._registered=!0}_setUpdateStrategy(){this.options&&null!=this.options.updateOn&&(this.control._updateOn=this.options.updateOn)}_isStandalone(){return!this._parent||!(!this.options||!this.options.standalone)}_setUpStandalone(){var t,e;(function(t,e,n){const r=function(t){return t._rawValidators}(t);null!==e.validator?t.setValidators(Xl(r,e.validator)):"function"==typeof r&&t.setValidators([r]);const s=function(t){return t._rawAsyncValidators}(t);null!==e.asyncValidator?t.setAsyncValidators(Xl(s,e.asyncValidator)):"function"==typeof s&&t.setAsyncValidators([s]);{const n=()=>t.updateValueAndValidity();ou(e._rawValidators,n),ou(e._rawAsyncValidators,n)}})(t=this.control,e=this),e.valueAccessor.writeValue(t.value),function(t,e){e.valueAccessor.registerOnChange(n=>{t._pendingValue=n,t._pendingChange=!0,t._pendingDirty=!0,"change"===t.updateOn&&iu(t,e)})}(t,e),function(t,e){const n=(t,n)=>{e.valueAccessor.writeValue(t),n&&e.viewToModelUpdate(t)};t.registerOnChange(n),e._registerOnDestroy(()=>{t._unregisterOnChange(n)})}(t,e),function(t,e){e.valueAccessor.registerOnTouched(()=>{t._pendingTouched=!0,"blur"===t.updateOn&&t._pendingChange&&iu(t,e),"submit"!==t.updateOn&&t.markAsTouched()})}(t,e),function(t,e){if(e.valueAccessor.setDisabledState){const n=t=>{e.valueAccessor.setDisabledState(t)};t.registerOnDisabledChange(n),e._registerOnDestroy(()=>{t._unregisterOnDisabledChange(n)})}}(t,e),this.control.updateValueAndValidity({emitEvent:!1})}_checkForErrors(){this._isStandalone()||this._checkParentType(),this._checkName()}_checkParentType(){}_checkName(){this.options&&this.options.name&&(this.name=this.options.name),this._isStandalone()}_updateValue(t){mu.then(()=>{this.control.setValue(t,{emitViewToModelChange:!1})})}_updateDisabled(t){const e=t.isDisabled.currentValue,n=""===e||e&&"false"!==e;mu.then(()=>{n&&!this.control.disabled?this.control.disable():!n&&this.control.disabled&&this.control.enable()})}}return t.\u0275fac=function(e){return new(e||t)(Zs(eu,9),Zs($l,10),Zs(zl,10),Zs(Ll,10))},t.\u0275dir=zt({type:t,selectors:[["","ngModel","",3,"formControlName","",3,"formControl",""]],inputs:{name:"name",isDisabled:["disabled","isDisabled"],model:["ngModel","model"],options:["ngModelOptions","options"]},outputs:{update:"ngModelChange"},exportAs:["ngModel"],features:[Co([yu]),Ps,ne]}),t})(),bu=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=Zt({type:t}),t.\u0275inj=ut({}),t})(),wu=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=Zt({type:t}),t.\u0275inj=ut({imports:[[bu]]}),t})(),Cu=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=Zt({type:t}),t.\u0275inj=ut({imports:[wu]}),t})(),Eu=(()=>{class t{constructor(){this.name=""}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275cmp=Ft({type:t,selectors:[["app"]],decls:5,vars:2,consts:[["placeholder","name",3,"ngModel","ngModelChange"]],template:function(t,e){1&t&&(Gs(0,"label"),oo(1,"Enter name:"),Ws(),Gs(2,"input",0),qs("ngModelChange",function(t){return e.name=t}),Ws(),Gs(3,"h2"),oo(4),Ws()),2&t&&(Sr(2),$s("ngModel",e.name),Sr(2),io("Hello, ",e.name,"!"))},directives:[Zl,su,vu],encapsulation:2}),t})(),Au=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=Zt({type:t,bootstrap:[Eu]}),t.\u0275inj=ut({imports:[[Rl,Cu]]}),t})();!function(){if(Wi)throw new Error("Cannot enable prod mode after platform setup.");Gi=!1}
/**
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */(),Nl().bootstrapModule(Au)},zn8P:function(t,e){function n(t){return Promise.resolve().then(function(){var e=new Error("Cannot find module '"+t+"'");throw e.code="MODULE_NOT_FOUND",e})}n.keys=function(){return[]},n.resolve=n,t.exports=n,n.id="zn8P"}},[[0,0]]]);