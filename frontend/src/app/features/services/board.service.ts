import { inject, Injectable, signal } from "@angular/core";
import { Lesson } from "../../shared/models/lesson.model";
import { LessonService } from "./lesson.service";

@Injectable({
  providedIn: 'root'
})
export class BoardService{
    private lessonService = inject(LessonService);  

    lessons = signal<Lesson[]>([]);

    setLessons(lessons: Lesson[]) {
        this.lessons.set(lessons);
    }

    loadUserLessons(userId: number) {
        this.lessons.set(this.lessonService.getUserSchedule(userId));
    }
}