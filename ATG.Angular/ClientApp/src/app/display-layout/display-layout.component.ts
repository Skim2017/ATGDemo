import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-display-layout',
  templateUrl: './display-layout.component.html',
  styles: ['canvas { border-style: solid }'],
    styleUrls: ['./display-layout.component.scss']
})

export class DisplayLayoutComponent implements OnInit {
  ngOnInit(): void {
    var canvas1 = <HTMLCanvasElement>document.getElementById('myCanvas');
    var context = canvas1.getContext('2d');
    context.fillStyle = '#2643ff';
    context.fillRect(0, 0, 300, 300);
    context.fillStyle = '#e5ff26'
    context.fillRect(60, 30, 180, 90);
  }

  animate(): void {
  }
/** display-layout ctor */
    constructor() {

    }
}
