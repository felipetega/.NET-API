import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CityCardComponent } from './components/city-card/city-card.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CityCardComponent,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AngularApp';
}
