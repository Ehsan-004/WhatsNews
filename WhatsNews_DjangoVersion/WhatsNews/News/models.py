from django.db import models


class Category(models.Model):
    name = models.CharField(max_length=100)

    class Meta:
        db_table = 'category'
        verbose_name = 'category'
        verbose_name_plural = 'categories'

    def __str__(self):
        return self.name


class Tag(models.Model):
    name = models.CharField(max_length=100)

    class Meta:
        db_table = 'tag'
        verbose_name = 'tag'
        verbose_name_plural = 'tags'

    def __str__(self):
        return self.name


class Article(models.Model):
    types = [
        ('N', 'Normal'),
        ('P', 'Picture'),
        ('V', 'Video'),
        ('R', 'Report'),
        ('I', 'Interview'),
    ]

    category = models.ForeignKey(Category, on_delete=models.CASCADE)
    title = models.CharField(max_length=100)
    image = models.ImageField(upload_to='article_images/')
    content = models.TextField()
    short_description = models.CharField(max_length=150, null=True, blank=True)
    create_date = models.DateTimeField(auto_now_add=True)
    like_count = models.IntegerField(default=0)
    view_count = models.IntegerField(default=0)
    first_level_index = models.BooleanField(default=False)
    second_level_index = models.BooleanField(default=False)
    third_level_index = models.BooleanField(default=False)
    type = models.CharField(max_length=1, choices=types, default='N')
    tags = models.ManyToManyField(Tag)

    class Meta:
        db_table = 'article'
        verbose_name = 'article'
        verbose_name_plural = 'articles'

    def __str__(self):
        return self.title


class Comment(models.Model):
    post = models.ForeignKey(Article, related_name='comments', on_delete=models.CASCADE, blank=True, null=True)
    auther_name = models.CharField(max_length=100, blank=True, null=True)
    auther_email = models.EmailField(blank=True, null=True)
    comment = models.TextField(null=True, blank=True)
    create_date = models.DateTimeField(auto_now_add=True, blank=True, null=True)
