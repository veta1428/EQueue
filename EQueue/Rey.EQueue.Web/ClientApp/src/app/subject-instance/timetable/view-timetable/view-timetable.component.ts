import { Component, Input, OnInit } from '@angular/core';
import { TimetableModel } from '../../../models/subject-instance'

@Component({
  selector: 'app-view-timetable',
  templateUrl: './view-timetable.component.html',
  styleUrls: ['./view-timetable.component.scss']
})
export class ViewTimetableComponent implements OnInit {

  @Input() timetable: TimetableModel = null!;

  public displayedColumns: string[] = ['dayOfWeek', 'startTime', 'duration'];

  constructor() { }

  ngOnInit(): void {

  }

}
