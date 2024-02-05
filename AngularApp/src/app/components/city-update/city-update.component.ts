import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component , OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-city-update',
  standalone: true,
  imports: [
    HttpClientModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './city-update.component.html',
  styleUrl: './city-update.component.css'
})
export class CityUpdateComponent {
  id: string = "";
  cityName: string = "";
  stateName: string = "";
  successMessage: string = '';

  constructor(private httpClient: HttpClient) {}

  updateCity(): void {
    const data = {
      id: this.id,
      cityName: this.cityName,
      stateName: this.stateName
    };
  
    this.httpClient.put(`api/${this.id}`, data).subscribe(
      (response) => {
        this.successMessage = 'City updated successfully!';
      },
      (error) => {
        console.error('Error updating city:', error);
        // Lógica de tratamento de erro, se necessário
      }
    );
  }

  closeSuccessMessage(): void {
    this.successMessage = '';
  }
}