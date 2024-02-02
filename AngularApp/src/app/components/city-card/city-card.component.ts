import { Component, OnInit, inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
    selector: 'app-city-card',
    standalone: true,
    imports: [
        HttpClientModule
    ],
    templateUrl: './city-card.component.html',
    styleUrl: './city-card.component.css'
})
export class CityCardComponent implements OnInit {

    httpClient = inject(HttpClient)
    data: any = []

    ngOnInit(): void {
        this.fetchData()
    }

    fetchData(){
        this.httpClient.get("/api").subscribe((data: any) => {
            console.log(data);
            this.data = data;
        });
    }
}
