import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'dropdown',
  templateUrl: './dropdown.component.html',
  imports: [CommonModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropdownComponent),
      multi: true,
    },
  ],
})
export class DropdownComponent implements ControlValueAccessor {
  writeValue(obj: any): void {
    this.value = obj;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  @Input() loadData: () => Observable<any[]> = () => of([]);
  @Input() loadSubData: (id: number) => Observable<any[]> = () => of([]);
  value?: any;
  private onChange: (value: any) => void = () => {};
  private onTouch: () => void = () => {};
  subData$: Observable<any[]> = of([]);
  data$: Observable<any[]> = of([]);
  hoverDataId?: number;

  removeData() {
    this.data$ = of([]);
    this.subData$ = of([]);
  }

  chooseSubdata(subData: any) {
    this.value = subData;
    this.onChange(this.value);
    this.removeData();
  }

  mouseEnterDropdown() {
    this.data$ = this.loadData();
  }

  mouseEnterData(id: number) {
    this.hoverDataId = id;
    this.subData$ = this.loadSubData(id);
  }

  mouseLeaveData() {
    this.hoverDataId = undefined;
    this.removeData();
  }
}
