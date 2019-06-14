import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

@Injectable()
export class HomeComponent implements OnInit{
  public visitor: Visitor = { visitorID: 1, ipAddress: "127.0.0.0.1", os: "OS", browser: "Browser", sexID: 1, malePercent: 50, femalePercent: 50 };
  public colorChangeEvent: BehaviorSubject<boolean>;
  

  //private url: string = "https://atgwebapp.azurewebsites.net/api/home/index";
  private url: string = "https://localhost:44377/api/home/index";

  constructor(private httpClient: HttpClient) { }

  onSubmit(visitorUpdated: Visitor, value: number) {
    visitorUpdated.sexID = value;
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = { headers: headers };
    this.httpClient.post<Visitor>(this.url, visitorUpdated, options).subscribe(result => { this.visitor = result as Visitor });
  }

  on

  ngOnInit() {
    this.httpClient.get<Visitor>(this.url).subscribe(result => { this.visitor = result as Visitor });
    this.colorChangeEvent = new BehaviorSubject<boolean>(false);
    this.colorChangeEvent.subscribe((data) => { data });
    this.colorChangeEvent.next(this.visitor.malePercent > 50);
    //this.colorChangeEvent.next(this.visitor.femalePercent > 50);
    //this.testScubscriber.next(this.visitor.malePercent > 50);
  }

  public femaleIcon = "assets/female_icon_512_512.png";
  public maleIcon = "assets/male-icon_512_512.jpg";
  public isMaleGreater = this.visitor.malePercent > 50;
  public isFemaleGreater = this.visitor.femalePercent > 50;
}

export interface Visitor {
  visitorID: number;
  ipAddress: string;
  os: string;
  browser: string;
  sexID?: number;
  malePercent: number;
  femalePercent: number;
}
