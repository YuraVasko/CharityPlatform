import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Organization } from '../models/organization'; 

 @Component({
  selector: 'app-organization-list',
  templateUrl: './organization-list.component.html',
  styleUrls: ['./organization-list.component.css']
})
export class OrganizationListComponent implements AfterViewInit {
  displayedColumns: string[] = ['name', 'mastersCount', 'projectsCount', 'createdAt'];
  dataSource: MatTableDataSource<Organization>;
  organizations: Organization[];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor() {
    this.organizations = [
      { id: '1234', name: 'Лікарня швидкої допомоги #1', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '1', name: 'Лікарня швидкої допомоги #2', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '2', name: 'Лікарня швидкої допомоги #3', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '3', name: 'Охмадит', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '4', name: 'Поліклініка на Богдана Хмельницького', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '5', name: 'Поліклініка на Топольній', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '6', name: 'Поліклініка на Лемківській', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '7', name: 'Project 8', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '8', name: 'Project 9', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '9', name: 'Project 10', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '10', name: 'Project 11', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '11', name: 'Project 12', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '12', name: 'Project 13', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '13', name: 'Project 14', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '14', name: 'Project 15', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '15', name: 'Project 16', mastersCount: 10, projectsCount: 15, createdAt: new Date()},
      { id: '16', name: 'Project 17', mastersCount: 10, projectsCount: 15, createdAt: new Date()}
    ];

    this.dataSource = new MatTableDataSource(this.organizations);
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
