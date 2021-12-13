import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Donor } from '../models/donor'; 

 @Component({
  selector: 'app-donors-list',
  templateUrl: './donors-list.component.html',
  styleUrls: ['./donors-list.component.css']
})
export class DonorsListComponent implements AfterViewInit {
  displayedColumns: string[] = ['email', 'firstName', 'lastName', 'donorLevel', 'actions'];
  dataSource: MatTableDataSource<Donor>;
  donors: Donor[];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor() {
    // Create 100 users
    this.donors = [
      { email: 'yuravasko2016@gmail.com', firstName: 'Yurii', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'petrovasko2016@gmail.com', firstName: 'Petro', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'andriivasko@gmail.com', firstName: 'Andrii', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'olenavasko@gmail.com', firstName: 'Olena', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'yuliavasko@gmail.com', firstName: 'Yulia', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'tarasvasko@gmail.com', firstName: 'Taras', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'sofiavasko@gmail.com', firstName: 'Sofia', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'nazarvasko@gmail.com', firstName: 'Nazar', lastName: 'Vasko', donorLevel: 'Beginer'},
      { email: 'test@gmail.com', firstName: 'Test', lastName: 'Vasko', donorLevel: 'Beginer'}
    ];

    this.dataSource = new MatTableDataSource(this.donors);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}