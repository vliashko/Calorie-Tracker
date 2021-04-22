import { Component } from '@angular/core';

@Component({
    selector: 'app',
    template: `<label>Enter name:</label>
                 <input [(ngModel)]="name" placeholder="name">
                 <h2>Hello, {{name}}!</h2>`
})
export class AppComponent {
    name = '';
}