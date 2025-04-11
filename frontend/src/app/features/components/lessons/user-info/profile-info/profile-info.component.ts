import { Component, computed, inject, input, OnInit, output, signal } from '@angular/core';
import { SubjectInfoService } from '../../../../services/subject-info.service.ts.service';
import { Subject } from '../../../../../shared/models/subject.model';
import { ModalState } from '../../models/modal-state.enum';
import { BoardService } from '../../../../services/board.service';

@Component({
  selector: 'app-profile-info',
  standalone: true,
  imports: [],
  templateUrl: './profile-info.component.html',
  styleUrl: './profile-info.component.css'
})
export class ProfileInfoComponent implements OnInit{

  private boardService = inject(BoardService);
  private subjectInfoService = inject(SubjectInfoService);

  userId = input.required<number>();
  
  lessons = computed(() => this.boardService.lessons());

  ngOnInit(): void {
    this.boardService.loadUserLessons(this.userId());
  }

  getUniqueSubjects(): Subject[] {
    return this.subjectInfoService.getUniqueSubjects(this.lessons());
  }
  
  getSubjectMinMaxYear(subject: Subject) {
    return this.subjectInfoService.getSubjectMinMaxYear(this.lessons(), subject);
  }
}
