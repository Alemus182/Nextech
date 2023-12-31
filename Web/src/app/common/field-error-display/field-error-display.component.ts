import { Component, OnInit, Input } from '@angular/core';
import {ValidationErrors} from '@angular/forms';

@Component({
  selector: 'app-field-error-display',
  templateUrl: './field-error-display.component.html',
  styleUrls: ['./field-error-display.component.css']
})
export class FieldErrorDisplayComponent implements OnInit {
  @Input() errorMsg: string;
  @Input() displayError: boolean;
  @Input() errors: Array<string>;
  
  constructor() { }

  ngOnInit(): void {
  
  }
}
