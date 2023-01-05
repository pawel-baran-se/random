from distutils.core import setup
import py2exe

setup(
    options = {'py2exe': {'bundle_files': 0, 'compressed': True}},
    windows=['MomentOfInertiaGUI.py'])
