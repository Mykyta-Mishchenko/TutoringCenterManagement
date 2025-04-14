import { inject, Injectable, signal } from "@angular/core";
import { Lesson } from "../../shared/models/lesson.model";
import { LessonService } from "./lesson.service";
import { map, Observable, tap } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BoardService{
    private lessonService = inject(LessonService);  

    lessons = signal<Lesson[]>([]);

    setLessons(lessons: Lesson[]) {
        this.lessons.set(lessons);
    }

    loadUserLessons(userId: number): Observable<Lesson[]> {
        return this.lessonService.getUserSchedule(userId).pipe(
            tap(lessons => this.lessons.set(lessons))
        )
    }
}