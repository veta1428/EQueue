import { Component, Input, OnInit } from '@angular/core';
import { Class } from '../../../models/subject-instance';
import moment from 'moment';

@Component({
  selector: 'app-view-classes',
  templateUrl: './view-classes.component.html',
  styleUrls: ['./view-classes.component.scss']
})
export class ViewClassesComponent implements OnInit {

  constructor() { }

  public getDateFormatted(date: string)
  {
      return moment.utc(date).local().format('H:mm');
  }

  @Input() classes: Class[] = null!;

  public displayedColumns: string[] = ['dayOfWeek', 'startTime', 'duration'];

  ngOnInit(): void {
  }

}
