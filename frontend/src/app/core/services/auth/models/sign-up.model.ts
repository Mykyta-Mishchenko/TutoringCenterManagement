export interface SignUpModel{
    firstName: string;
    lastName: string;
    email: string;
    role: 'student' | 'teacher';
    password: string;
    confirmedPassword: string;
}