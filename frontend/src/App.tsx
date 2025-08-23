import { useState } from 'react';
import './App.css';
import Login from './components/login';
import Registration from './components/registration';

function App() {
    const [activeTab, setActiveTab] = useState<'login' | 'registration'>('login');

    return (
        <div style={{ 
            minHeight: '100vh', 
            padding: '2rem 1rem',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            gap: '2rem'
        }}>
            <h1 style={{ 
                textAlign: 'center', 
                color: '#1f2937', 
                fontSize: '2.5rem',
                fontWeight: '700',
                margin: '0 0 2rem 0'
            }}>
                Task Management
            </h1>
            
            <div style={{
                maxWidth: '400px',
                width: '100%',
                background: 'white',
                borderRadius: '16px',
                overflow: 'hidden',
                boxShadow: '0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)'
            }}>
                <div style={{
                    display: 'flex',
                    background: '#f8fafc'
                }}>
                    <button
                        onClick={() => setActiveTab('login')}
                        style={{
                            flex: 1,
                            padding: '1rem',
                            border: 'none',
                            background: activeTab === 'login' ? 'white' : 'transparent',
                            color: activeTab === 'login' ? '#3b82f6' : '#6b7280',
                            fontWeight: activeTab === 'login' ? '600' : '500',
                            cursor: 'pointer',
                            transition: 'all 0.2s ease',
                            borderBottom: activeTab === 'login' ? '2px solid #3b82f6' : '2px solid transparent'
                        }}
                    >
                        Sign In
                    </button>
                    <button
                        onClick={() => setActiveTab('registration')}
                        style={{
                            flex: 1,
                            padding: '1rem',
                            border: 'none',
                            background: activeTab === 'registration' ? 'white' : 'transparent',
                            color: activeTab === 'registration' ? '#10b981' : '#6b7280',
                            fontWeight: activeTab === 'registration' ? '600' : '500',
                            cursor: 'pointer',
                            transition: 'all 0.2s ease',
                            borderBottom: activeTab === 'registration' ? '2px solid #10b981' : '2px solid transparent'
                        }}
                    >
                        Sign Up
                    </button>
                </div>
                
                <div style={{ padding: '0' }}>
                    {activeTab === 'login' ? (
                        <Login onSwitchToRegistration={() => setActiveTab('registration')} />
                    ) : (
                        <Registration onSwitchToLogin={() => setActiveTab('login')} />
                    )}
                </div>
            </div>
        </div>
    );
}

export default App;
