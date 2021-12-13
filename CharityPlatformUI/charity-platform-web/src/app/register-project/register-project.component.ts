import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,Validators } from '@angular/forms';
import { OrgType } from '../models/organization-type';

@Component({
  selector: 'app-register-project',
  templateUrl: './register-project.component.html',
  styleUrls: ['./register-project.component.css']
})
export class RegisterProjectComponent implements OnInit {

  public registerProj!: FormGroup;

  constructor(
    private fb:FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initReisterForm();
  }
  orgType: OrgType[] = [
    { value: 'type-1', viewValue: 'церква Св.Анни' },
    { value: 'type-2', viewValue: 'Госпіталь ім А.Шептицького' },
    { value: 'type-3', viewValue: 'ЛНУ ім. І.Франка' },
  ]
  initReisterForm():void{
    this.registerProj = this.fb.group({
      projTitle: [null, Validators.required],
      projDesc: [null, Validators.required],
      orgSelect: [null, Validators.required],
      donationGoal: [null, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
    })
  }
  register():void{
    const {projTitle,projDesc,orgSelect,donationGoal,startDate,endDate } = this.registerProj.value;
    const newProject ={
      title: projTitle, 
      description:projDesc,
      organization: orgSelect, 
      donationSum: donationGoal,
      startDate: startDate, 
      endDate: endDate,
    }
    this.registerProj.reset();
    console.log(newProject);
  }

}
