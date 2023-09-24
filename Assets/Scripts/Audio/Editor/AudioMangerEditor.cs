using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( AudioManager ) )]
public class AudioMangerEditor : Editor
{


    private SerializedProperty settings;

    private void OnEnable()
    {
        settings = serializedObject.FindProperty( "settings" );
    }

    public override void OnInspectorGUI()
    {

        if ( Application.isPlaying )
        {
            EditorGUILayout.HelpBox( "Only editor mode can modify data.", MessageType.Info );
            return;
        }



        if ( null != settings && settings.isArray )
        {

            if ( GUILayout.Button( "New" ) )
            {
                settings.InsertArrayElementAtIndex( 0 );
            }


            for ( int i = 0; i < settings.arraySize; i++ )
            {


                var setting = settings.GetArrayElementAtIndex( i );
                var name = setting.FindPropertyRelative( "name" );
                var file = setting.FindPropertyRelative( "file" );

                EditorGUILayout.BeginHorizontal();

                if ( GUILayout.Button( "X", GUILayout.Width( 20 ) ) )
                {
                    settings.DeleteArrayElementAtIndex( i );
                    break;
                }

                name.stringValue = GUILayout.TextField( name.stringValue, GUILayout.Width( 200 ) );

                AudioClip clip = string.IsNullOrWhiteSpace( file.stringValue ) ? null : AssetDatabase.LoadAssetAtPath<AudioClip>( file.stringValue );

                clip = ( AudioClip ) EditorGUILayout.ObjectField( clip, typeof( AudioClip ), false );

                if ( GUI.changed && null != clip )
                {
                    string newFilePath = AssetDatabase.GetAssetPath( clip );
                    if ( newFilePath != file.stringValue )
                    {
                        name.stringValue = Path.GetFileNameWithoutExtension( newFilePath );
                    }
                    file.stringValue = newFilePath;
                }

                EditorGUILayout.EndHorizontal();

            }

        }


        serializedObject.ApplyModifiedProperties();

    }


}
