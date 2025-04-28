import { inject, Injectable } from '@angular/core';
import { Subject } from '../../shared/models/subject.model';
import { Lesson } from '../../shared/models/lesson.model';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { LessonEditDTO } from '../../shared/models/dto/lesson-edit.dto';
import { LessonCreateDTO } from '../../shared/models/dto/lesson-create.dto';

@Injectable({
  providedIn: 'root'
})
export class LessonService{

    private apiUrl = environment.apiUrl;
    private httpClient = inject(HttpClient);

    getSubjects(): Observable<Subject[]> {
        return this.httpClient.get<Subject[]>(`${this.apiUrl}/lessons/subjects`, { withCredentials: true });
    }

    getUserSchedule(userId: number): Observable<Lesson[]>{
        return this.httpClient.get<Lesson[]>(`${this.apiUrl}/lessons?userId=${userId}`, { withCredentials: true })
            .pipe(map(lessons =>
                lessons.map(lesson => ({
                    ...lesson,
                    schedule: {
                        ...lesson.schedule,
                        dayTime: new Date(lesson.schedule.dayTime)
                    }
                }))
            ));
    }

    updateLesson(lesson: LessonEditDTO): Observable<any> {
        return this.httpClient.put(`${this.apiUrl}/lessons/update`, lesson, { withCredentials: true });
    }

    addLesson(lesson: LessonCreateDTO): Observable<any> {
        return this.httpClient.post(`${this.apiUrl}/lessons/create`, lesson, { withCredentials: true });
    }

    deleteLesson(lessonId: number) {
        return this.httpClient.delete(`${this.apiUrl}/lessons/delete/${lessonId}`, { withCredentials: true });
    }

    unsubscribeLesson(lessonId: number) {
        return this.httpClient.delete(`${this.apiUrl}/lessons/unsubscribe/${lessonId}`, { withCredentials: true });
    }
    
    subscribeLesson(lessonId: number) : Observable<any> {
        return this.httpClient.post(`${this.apiUrl}/lessons/subscribe/${lessonId}` , { withCredentials: true });
    }
}