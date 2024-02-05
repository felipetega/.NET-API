import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-city-create',
  standalone: true,
  imports: [
    FormsModule,
    HttpClientModule,
    CommonModule
  ],
  templateUrl: './city-create.component.html',
  styleUrl: './city-create.component.css'
})
export class CityCreateComponent {

  newCity = { cityName: '', stateName: '' };
  successMessage: string = '';

  constructor(private httpClient: HttpClient) {}

  createCity() {
    this.httpClient.post('/api', this.newCity).subscribe(
      (response) => {
        console.log('City created successfully:', response);
        this.successMessage = 'City created successfully!';
      },
      (error) => {
        console.error('Error creating city:', error);
        this.successMessage = '';
      }
    );
  }

  closeSuccessMessage() {
    this.successMessage = '';
  }
}
