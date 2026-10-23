import { Injectable } from '@angular/core';
import { createClient, SupabaseClient, User, Session } from '@supabase/supabase-js';
import { environment } from '../../../environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root'})
export class SupabaseService
{
    private supabase: SupabaseClient;
    private currentUserSubject = new BehaviorSubject<User | null>(null);
    currentUser$ = this.currentUserSubject.asObservable();

    constructor() {
        this.supabase = createClient(environment.supabaseUrl, environment.supabaseAnonKey);
        

        //Keep session in sync
        this.supabase.auth.onAuthStateChange((event, session) => {
            this.currentUserSubject.next(session?.user ?? null);
        });

        //Load existing session on start
        this.supabase.auth.getSession().then(({ data }) => {
            this.currentUserSubject.next(data.session?.user ?? null);
        });
    }

    get client() {
        return this.supabase;
    }

    async getSession(): Promise<Session | null> {
        const { data } = await this.supabase.auth.getSession();
        return data.session;
    }

    async signUp(email: string, password: string, displayName?: string){
        return this.supabase.auth.signUp({
            email,
            password,
            options: {
                data : {displayName: displayName}
            }
        });
    }

    async signIn(email: string, password: string){
        return this.supabase.auth.signInWithPassword({ email, password});
    }

    async signOut(){
        return this.supabase.auth.signOut();
    }

}