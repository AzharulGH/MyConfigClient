import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
    selector: '[appDateFormat]'
})
export class DateFormatDirective {
    @Input('appDateFormat') formatStr: string = 'yyyy/MM/dd';

    constructor(private el: ElementRef) { }

    @HostListener('blur') onBlur() {
        let value: string = this.el.nativeElement.value.trim();
        if (!value) return;

        // Normalize separators (- or . → /)
        value = value.replace(/[-.]/g, '/');

        const parts = value.split('/');
        let yyyy, mm, dd;

        // Try to detect format (yyyy first or dd first)
        if (parts.length === 3) {
            if (parts[0].length === 4) {
                // yyyy/MM/dd
                yyyy = +parts[0];
                mm = +parts[1];
                dd = +parts[2];
            } else {
                // dd/MM/yyyy
                dd = +parts[0];
                mm = +parts[1];
                yyyy = +parts[2];
            }

            const date = new Date(yyyy, mm - 1, dd);
            if (!isNaN(date.getTime())) {
                this.el.nativeElement.value = this.formatDate(date, this.formatStr);
            }
        }
    }

    private formatDate(date: Date, format: string): string {
        const yyyy = date.getFullYear();
        const mm = ('0' + (date.getMonth() + 1)).slice(-2);
        const dd = ('0' + date.getDate()).slice(-2);

        return format
            .replace('yyyy', yyyy.toString())
            .replace('MM', mm)
            .replace('dd', dd);
    }
}
