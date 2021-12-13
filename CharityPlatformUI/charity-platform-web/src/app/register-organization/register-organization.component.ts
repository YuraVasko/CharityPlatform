import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { OrgType } from '../models/organization-type';
import { Masters } from '../models/Masters';

@Component({
  selector: 'app-register-organization',
  templateUrl: './register-organization.component.html',
  styleUrls: ['./register-organization.component.css']
})
export class RegisterOrganizationComponent implements OnInit {

  public registerOrg!: FormGroup;

  constructor(
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initRegisterForm();
  }

  masters: Masters[] = [
    { value: 'master-1', viewValue: 'Yurii Vasko' },
    { value: 'master-2', viewValue: 'Yurii Tanavsky' },
    { value: 'master-3', viewValue: 'Yurii Spasokukotsky (chertyaka shchekastyi)' },
    { value: 'master-4', viewValue: 'Yurii Gagarin' },
  ]
  orgType: OrgType[] = [
    { value: 'type-1', viewValue: 'Hospital' },
    { value: 'type-2', viewValue: 'Orphanage' },
    { value: 'type-3', viewValue: 'Church' },
  ]
  initRegisterForm(): void {
    this.registerOrg = this.fb.group({
      orgName: [null, Validators.required],
      orgDesc: [null, Validators.required],
      mastersSelect: [null, Validators.required],
      orgTypeSel: [null, Validators.required],
    })
  }
  
  register(): void{
    const {orgName, orgDesc, mastersSelect, orgTypeSel} = this.registerOrg.value;
    console.log(orgName, orgDesc, mastersSelect,  orgTypeSel);
    this.registerOrg.reset();
  }
}
