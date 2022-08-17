import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unauthorized-access',
  templateUrl: './unauthorized-access.component.html',
  styleUrls: ['./unauthorized-access.component.css']
})
export class UnauthorizedAccessComponent implements OnInit {

  constructor(private router: Router) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.router.navigate(['login']);
  }, 5000);
  }

}
