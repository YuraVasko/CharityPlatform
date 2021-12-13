import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  public UserYurii ={
    login: 'yura@gmail.com',
    password: 'q12q12q12',
  }
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.initRegisterForm()
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
    this.loginForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(4)]],
    })
  }
  logIn(): void{
    const {email, password} = this.loginForm.value;
    if(email === this.UserYurii.login && password === this.UserYurii.password){
      this.toastr.success('Hello Yurii!') 
    } else{
      this.toastr.error("I don't recognize you!")
    }
    this.loginForm.reset();
  }

}
