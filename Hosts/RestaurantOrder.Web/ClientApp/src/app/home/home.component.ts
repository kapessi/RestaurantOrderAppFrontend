import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public menu = "";
  public http: HttpClient;
  public baseUrl = "";
  public dayTimes = [
    { name: "Morning", value: "morning" },
    { name: "Night", value: "night" }
  ];
  public selectedDayTime: string;
  public menuOptions: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  public getMenu() {
    this.http.get<string>(
      this.baseUrl + 'home',
      {
        params:
        {
          DayTime: this.selectedDayTime,
          DishTypes: this.menuOptions
        }
      }).subscribe(
        result => { this.menu = result; },
        error => console.error(error));
  }
}
