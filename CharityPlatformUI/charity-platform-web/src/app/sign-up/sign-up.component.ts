import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  
  public signUpForm!: FormGroup;
  
  constructor(
    private fb: FormBuilder,
    
  ) { }

  ngOnInit(): void {
    this.initRegisterForm();
  }

  email = new FormControl('', [Validators.required, Validators.email]);
  hide = true;
  
  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }
  initRegisterForm(): void{
    this.signUpForm = this.fb.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(4)]],
    })
  }
  register(): void{
    const { firstName, lastName, email, password} = this.signUpForm.value;
    const user ={
      firstName : firstName,
      lastName : lastName,
      email : email,
      role: 'USER',
    }
    console.log(user, password);
    this.signUpForm.reset();
    
  }

}