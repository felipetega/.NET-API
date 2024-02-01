import { Component, OnInit, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Init } from 'v8';

@Component({
  selector: 'app-city-card',
  standalone: true,
  imports: [

  ],
  templateUrl: './city-card.component.html',
  styleUrl: './city-card.component.css'
})
export class CityCardComponent implements OnInit {

  httpClient = inject(HttpClient)

  ngOnInit(): void {}

  fetchData(){
    this.httpClient.get("")
  }

}
