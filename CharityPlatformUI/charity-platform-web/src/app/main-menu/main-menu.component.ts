import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user-service/user.service';
import { MatMenuItem } from '@angular/material/menu'

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.css']
})
export class MainMenuComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.setUpUserMenu();
    this.userService.loggedEmmit.subscribe(() => {
      this.setUpUserMenu();
    });
  }

  setUpUserMenu() {
    this.userRole = "admin";
    this.isAuthorize = true;
  }

  logOutClick() {
    this.userService.signOut();
  }

  manageUsersClick(){
    this.router.navigate(['/user/list']);
  }
  
  userRole = "admin";
  isAuthorize = true;
}