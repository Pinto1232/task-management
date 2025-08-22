module.exports = function (plop) {
    // Create generators here
    plop.setGenerator('component', {
        description: 'Create a new React component',
        prompts: [
            {
                type: 'input',
                name: 'name',
                message: 'Component name (camelCase):',
                validate: (value) => {
                    if (!/^[a-z][A-Za-z0-9]*$/.test(value)) {
                        return 'Invalid component name. Must be camelCase and start with a lowercase letter.';
                    } else {
                        return true;
                    }
                },
            },
            {
                type: 'input',
                name: 'description',
                message: 'Component description:',
            },
        ],
        actions: [
            {
                type: 'add',
                path: 'src/components/{{ camelCase name}}/{{camelCase name}}.tsx',
                templateFile: 'plop-templates/component.hbs',   
            },
            {
                type: 'add',
                path: 'src/components/{{ camelCase name}}/{{camelCase name}}.styles.css',
                templateFile: 'plop-templates/styles.hbs',   
            },
            {
                type: 'add',
                path: 'src/components/{{ camelCase name}}/{{camelCase name}}.types.ts',
                templateFile: 'plop-templates/types.hbs',   
            },
             {
                type: 'add',
                path: 'src/components/{{ camelCase name}}/{{camelCase name}}.test.tsx',
                templateFile: 'plop-templates/test.hbs',   
            },
             {
                type: 'add',
                path: 'src/components/{{ camelCase name}}/{{camelCase name}}.index.ts',
                templateFile: 'plop-templates/index.hbs',   
            }
        ],
    });
    
    // UI Component generator
    plop.setGenerator('ui-component', {
        description: 'Create a new UI component sr/ui/',
        prompts: [
            {
                type: 'input',
                name: 'name',
                message: 'UI Component name (camelCase):',
                validate: (value) => {
                    if (!/^[a-z][A-Za-z0-9]*$/.test(value)) {
                        return 'Invalid UI component name. Must be camelCase and start with a lowercase letter.';
                    } else {
                        return true;
                    }
                },
            },
            {
                type: 'input',
                name: 'description',
                message: 'UI Component description:',
            },
        ],
        actions: [
            {
                type: 'add',    
                path: 'src/ui/{{ camelCase name}}/{{camelCase name}}.tsx',
                templateFile: 'plop-templates/component.hbs',   
            },
            {
                type: 'add',    
                path: 'src/ui/{{ camelCase name}}/{{camelCase name}}.styles.css',
                templateFile: 'plop-templates/styles.hbs',   
            },
            {
                type: 'add',    
                path: 'src/ui/{{ camelCase name}}/{{camelCase name}}.types.ts',
                templateFile: 'plop-templates/types.hbs',   
            },
            {
                type: 'add',    
                path: 'src/ui/{{ camelCase name}}/{{camelCase name}}.test.tsx',
                templateFile: 'plop-templates/test.hbs',   
            },
            {
                type: 'add',    
                path: 'src/ui/{{ camelCase name}}/{{camelCase name}}.index.tsx',
                templateFile: 'plop-templates/test.hbs',   
            }
        ]
    });
}