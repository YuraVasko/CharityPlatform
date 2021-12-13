import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { CreateCharityProject } from '../models/charity-project'; 
import { PaymentFormComponent } from '../payment-form/payment-form.component';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements AfterViewInit {
  displayedColumns: string[] = ['title', 'organization', 'goal', 'alreadyDonated', 'dueDate', 'actions'];
  dataSource: MatTableDataSource<CreateCharityProject>;
  projects: CreateCharityProject[];

  data : string = "eyJwdWJsaWNfa2V5Ijoic2FuZGJveF9pMjE0MTkzMzYyNzMiLCJ2ZXJzaW9uIjoiMyIsImFjdGlvbiI6InBheSIsImFtb3VudCI6MTUsImN1cnJlbmN5IjoiVUFIIiwiZGVzY3JpcHRpb24iOiJEZXNjcmlwdGlvbiIsIm9yZGVyX2lkIjoiM2ZhODVmNjQtNTcxNy00NTYyLWIzZmMtMmM5NjNmNjZhZmE2LTNmYTg1ZjY0LTU3MTctNDU2Mi1iM2ZjLTJjOTYzZjY2YWZhNmJiOTczYzFmLWIyNjAtNGI5Yi1iYjVlLTY0NjZhMzg0NjlhMyIsInBheXR5cGVzIjoiY2FyZCJ9";
  signature : string = "PbtImvmYJqwKiYxaHTNcukzPvho=";

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(public dialog: MatDialog) {
    this.projects = [
      { id: '1234', title: 'Ремонт в приймалому відділені', organization: 'Лікарня швидкої допомоги #1', alreadyDonated: 500, dueDate: new Date(), goal: 1500 },
      { id: '1', title: 'Ремонт на сходовій клітці', organization: 'Лікарня швидкої допомоги #1', alreadyDonated: 100, dueDate: new Date(), goal: 2500},
      { id: '2', title: 'Рентген', organization: 'Лікарня швидкої допомоги #3', alreadyDonated: 150, dueDate: new Date(), goal: 8000 },
      { id: '3', title: 'Стоматологічне крісло', organization: 'Поліклініка на Лемківській', alreadyDonated: 190, dueDate: new Date(), goal: 200 },
      { id: '4', title: 'Тест', organization: 'Лікарня швидкої допомоги #3', alreadyDonated: 80, dueDate: new Date(), goal: 9000 },
      { id: '5', title: 'Project 6', organization: 'organization6', alreadyDonated: 100, dueDate: new Date(), goal: 1000 },
      { id: '6', title: 'Project 7', organization: 'organization7', alreadyDonated: 75, dueDate: new Date(), goal: 500 },
      { id: '7', title: 'Project 8', organization: 'organization8', alreadyDonated: 40, dueDate: new Date(), goal: 800 },
      { id: '8', title: 'Project 9', organization: 'organization9', alreadyDonated: 30, dueDate: new Date(), goal: 4000 },
      { id: '9', title: 'Project 10', organization: 'organization10', alreadyDonated: 15, dueDate: new Date(), goal: 5000 },
      { id: '10', title: 'Project 11', organization: 'organization11', alreadyDonated: 25, dueDate: new Date(), goal: 12000 },
      { id: '11', title: 'Project 12', organization: 'organization12', alreadyDonated: 50, dueDate: new Date(), goal: 100000 },
      { id: '12', title: 'Project 13', organization: 'organization13', alreadyDonated: 100, dueDate: new Date(), goal: 500 },
      { id: '13', title: 'Project 14', organization: 'organization13', alreadyDonated: 200, dueDate: new Date(), goal: 300 },
      { id: '14', title: 'Project 15', organization: 'organization14', alreadyDonated: 150, dueDate: new Date(), goal: 7000 },
      { id: '15', title: 'Project 16', organization: 'organization15', alreadyDonated: 40, dueDate: new Date(), goal: 6500 },
      { id: '16', title: 'Project 17', organization: 'organization16', alreadyDonated: 50, dueDate: new Date(), goal: 1500 }
    ];

    this.dataSource = new MatTableDataSource(this.projects);
  }

  openDialog(projectId : string): void {
    const dialogRef = this.dialog.open(PaymentFormComponent, {
      width: '250px',
      data: {projectId: projectId},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');

      if(result != null)
      {
        this.submitPatmentForm();
      }
    });
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

  submitPatmentForm(): void{
    window.location.href = "https://www.liqpay.ua/api/3/checkout" + "?data=" + this.data + "&signature=" + this.signature;
  }
}