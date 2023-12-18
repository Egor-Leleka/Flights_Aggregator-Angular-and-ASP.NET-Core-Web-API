import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { PassengerService } from 'src/app/services/passenger/passenger.service';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent implements OnInit{

  constructor(private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }
  
  requestedUrl?: string = undefined
  
  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
    firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
    lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
    isFemale: [true, Validators.required]
  })
  
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl'])
  }
    
  checkPassenger(): void {
    const email = this.form.get('email')?.value!;
 
    if(!email) return;
  
    this.passengerService
      .findPassenger(email)
      .subscribe(
        this.login, e => {
          if (e.staus != 404)
            console.error(e)
        }
      )
  }

  register(): void {
    if (this.form.invalid) return;
  
    const formValue = this.form.value;
    console.log("Form Values: ", formValue);
  
    this.passengerService.registerPassenger(formValue)
      .subscribe(
        this.login,
        error => console.error(error)
      );
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value! });
    this.router.navigate([this.requestedUrl ?? '/search-flights'])
  }

}
