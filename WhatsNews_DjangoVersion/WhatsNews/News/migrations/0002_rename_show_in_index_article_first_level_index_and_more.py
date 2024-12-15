# Generated by Django 5.0.2 on 2024-11-28 10:31

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('News', '0001_initial'),
    ]

    operations = [
        migrations.RenameField(
            model_name='article',
            old_name='show_in_index',
            new_name='first_level_index',
        ),
        migrations.AddField(
            model_name='article',
            name='second_level_index',
            field=models.BooleanField(default=False),
        ),
        migrations.AddField(
            model_name='article',
            name='third_level_index',
            field=models.BooleanField(default=False),
        ),
        migrations.AddField(
            model_name='article',
            name='view_count',
            field=models.IntegerField(default=0),
        ),
    ]
