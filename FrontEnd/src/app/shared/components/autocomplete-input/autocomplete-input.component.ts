import {
  Component,
  Input,
  Output,
  EventEmitter,
  forwardRef,
  OnInit,
} from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  FormControl,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-autocomplete-input',
  standalone: false,
  templateUrl: './autocomplete-input.component.html',
  styleUrls: ['./autocomplete-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AutocompleteInputComponent),
      multi: true,
    },
  ],
})
export class AutocompleteInputComponent implements ControlValueAccessor, OnInit {
  @Input() label = '';
  @Input() placeholder = '';
  @Input() options: string[] = [];
  @Input() required = false;
  @Input() disabled = false;

  @Output() optionSelected = new EventEmitter<string>();

  control = new FormControl<string>('');
  filteredOptions$!: Observable<string[]>;

  private onChange: (value: string | null) => void = () => {};
  private onTouched: () => void = () => {};

  ngOnInit(): void {
    this.filteredOptions$ = this.control.valueChanges.pipe(
      startWith(''),
      map((value) => this.filter(value ?? ''))
    );
  }

  writeValue(value: string | null): void {
    this.control.setValue(value ?? '', { emitEvent: false });
  }

  registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    if (isDisabled) {
      this.control.disable();
    } else {
      this.control.enable();
    }
  }

  onOptionSelect(event: MatAutocompleteSelectedEvent): void {
    const value = event.option.value as string;
    this.onChange(value);
    this.onTouched();
    this.optionSelected.emit(value);
  }

  onBlur(): void {
    this.onTouched();
    const current = this.control.value;
    if (current && !this.options.includes(current)) {
      this.onChange(current);
    } else if (current) {
      this.onChange(current);
    }
  }

  onInput(): void {
    this.onChange(this.control.value);
  }

  displayFn(value: string): string {
    return value ?? '';
  }

  private filter(value: string): string[] {
    const lower = value.toLowerCase();
    return this.options.filter((opt) => opt.toLowerCase().includes(lower));
  }
}
