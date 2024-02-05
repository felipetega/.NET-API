import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-city-delete',
  standalone: true,
  imports: [
    HttpClientModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './city-delete.component.html',
  styleUrl: './city-delete.component.css'
})
export class CityDeleteComponent {
  id: string = "";
  successMessage: string = "";

  constructor(private httpClient: HttpClient){}

  deleteCity(): void {
    const data = {
      id: this.id
    }

    this.httpClient.delete(`api/${this.id}`).subscribe(
      (response) => {
        this.successMessage = 'City deleted successfully!';
      },
      (error) => {
        console.error('Error updating city:', error);
      }
    );
  }

  closeSuccessMessage() {
    this.successMessage = '';
  }

}
